# Ransomware

Ransomware is a project written in .Net and shows how ransomeware generally works. This repository should be used for educational reasons only!!

[![Build status](https://ci.appveyor.com/api/projects/status/m435h8vg3m6uwdv2?svg=true)](https://ci.appveyor.com/project/SeppPenner/ransomware)
[![GitHub issues](https://img.shields.io/github/issues/SeppPenner/Ransomware.svg)](https://github.com/SeppPenner/Ransomware/issues)
[![GitHub forks](https://img.shields.io/github/forks/SeppPenner/Ransomware.svg)](https://github.com/SeppPenner/Ransomware/network)
[![GitHub stars](https://img.shields.io/github/stars/SeppPenner/Ransomware.svg)](https://github.com/SeppPenner/Ransomware/stargazers)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://raw.githubusercontent.com/SeppPenner/Ransomware/master/License.txt)
[![Known Vulnerabilities](https://snyk.io/test/github/SeppPenner/Ransomware/badge.svg)](https://snyk.io/test/github/SeppPenner/Ransomware)
[![Blogger](https://img.shields.io/badge/Follow_me_on-blogger-orange)](https://franzhuber23.blogspot.de/)
[![Patreon](https://img.shields.io/badge/Patreon-F96854?logo=patreon&logoColor=white)](https://patreon.com/SeppPennerOpenSourceDevelopment)
[![PayPal](https://img.shields.io/badge/PayPal-00457C?logo=paypal&logoColor=white)](https://paypal.me/th070795)

## The stuff behind
The software is basically a "malware" version of https://github.com/SeppPenner/LustigeFehler that shows some nonsense error messages in the foreground, but encrypts data in the background.

* If the software is not run in admin mode, it will crash with an error.
* If the .exe is started in admin mode, it will run and try to encrypt all files on all drives it finds.
* Additionally, it will **hide** all folders it finds.

## Virustotal.com scans
Well, let's see what [virustotal.com](https://www.virustotal.com) shows us as information on this "virus":

* The result: https://www.virustotal.com/gui/file/d2a0638ffd29888de39fd138f81e667888b7c9f04649032a042bd558dec4bf98?nocache=1 --> 1 / 69 detections --> Very poor :D

## Hint
Please don't try this software on your PC. It's for educational purposes only!!!!!!

Change history
--------------

See the [Changelog](https://github.com/SeppPenner/Ransomware/blob/master/Changelog.md).