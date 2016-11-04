// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;

using Lyudmila.Client.Properties;

using SharpCompress.Archives;
using SharpCompress.Archives.Zip;
using SharpCompress.Readers;

namespace Lyudmila.Client.Helpers
{
    internal class UsernameUpdater
    {
        public static void SetName(string gameName)
        {
            var path = string.Empty;
            var newLine = string.Empty;
            var SettingsFile = new string[] {};
            var i = 0;

            Thread.Sleep(500);

            switch(gameName)
            {
                case "AoE2HD":
                    path = Path.Combine(Settings.Default.AoE2HD_Location, "SmartSteamEmu.ini");
                    SettingsFile = File.ReadAllLines(path);
                    i = 0;
                    foreach(var line in SettingsFile)
                    {
                        if(line.StartsWith("PersonaName = "))
                        {
                            newLine = line.Replace("Unreal", Settings.Default.Username);
                            SettingsFile[i] = newLine;
                            File.WriteAllLines(path, SettingsFile);
                            return;
                        }
                        i++;
                    }
                    break;
                case "BF3":
                    path = Path.Combine(Settings.Default.BF3_Location, "_StartGame.bat");
                    SettingsFile = File.ReadAllLines(path);
                    newLine = SettingsFile.First().Replace(@"logintoken=\""Unreal\""", $"logintoken = \\\"{Settings.Default.Username}\\\"");
                    SettingsFile[0] = newLine;
                    File.WriteAllLines(path, SettingsFile);
                    break;
                case "CSGO":
                    path = Path.Combine(Settings.Default.CSGO_Location, "rev.ini");
                    SettingsFile = File.ReadAllLines(path);
                    i = 0;
                    foreach (var line in SettingsFile)
                    {
                        if (line.StartsWith("PlayerName=Unreal"))
                        {
                            newLine = line.Replace("Unreal", Settings.Default.Username);
                            SettingsFile[i] = newLine;
                            File.WriteAllLines(path, SettingsFile);
                            return;
                        }
                        i++;
                    }
                    break;
                case "CoD2":
                    path = Path.Combine(Settings.Default.CoD2_Location, "main\\players\\Unreal\\config_mp.cfg");
                    SettingsFile = File.ReadAllLines(path);
                    i = 0;
                    foreach(var line in SettingsFile)
                    {
                        if(line.StartsWith("seta name \"Unreal\""))
                        {
                            newLine = line.Replace("Unreal", Settings.Default.Username);
                            SettingsFile[i] = newLine;
                            File.WriteAllLines(path, SettingsFile);
                            return;
                        }
                        i++;
                    }
                    break;
                case "CoD4":
                    path = Path.Combine(Settings.Default.CoD4_Location, "players\\profiles\\Unreal\\config_mp.cfg");
                    SettingsFile = File.ReadAllLines(path);
                    i = 0;
                    foreach(var line in SettingsFile)
                    {
                        if(line.StartsWith("seta name \"Unreal\""))
                        {
                            newLine = line.Replace("Unreal", Settings.Default.Username);
                            SettingsFile[i] = newLine;
                            File.WriteAllLines(path, SettingsFile);
                            return;
                        }
                        i++;
                    }
                    break;
                case "CoD5":
                    new WebClient().DownloadFile(new Uri($"http://{Settings.Default.ServerIP}/logiciels/launcher/CoD5.zip"), @"CoD5.zip");
                    using(var archive = ZipArchive.Open(Path.Combine(Environment.CurrentDirectory, "CoD5.zip")))
                    {
                        foreach(var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                        {
                            entry.WriteToDirectory(Environment.GetEnvironmentVariable("LocalAppData"),
                                new ExtractionOptions {ExtractFullPath = true, Overwrite = true});
                        }
                    }
                    File.Delete(Path.Combine(Environment.CurrentDirectory, "CoD5.zip"));
                    path = Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), "Activision\\CoDWaW\\players\\profiles\\$$$\\config.cfg");
                    SettingsFile = File.ReadAllLines(path);
                    i = 0;
                    foreach(var line in SettingsFile)
                    {
                        if(line.StartsWith("seta name \"Unreal\""))
                        {
                            newLine = line.Replace("Unreal", Settings.Default.Username);
                            SettingsFile[i] = newLine;
                            File.WriteAllLines(path, SettingsFile);
                            return;
                        }
                        i++;
                    }
                    break;
                case "DoDS":
                    path = Path.Combine(Settings.Default.DoDS_Location, "rev.ini");
                    SettingsFile = File.ReadAllLines(path);
                    i = 0;
                    foreach(var line in SettingsFile)
                    {
                        if(line.StartsWith("PlayerName = Unreal"))
                        {
                            newLine = line.Replace("Unreal", Settings.Default.Username);
                            SettingsFile[i] = newLine;
                            File.WriteAllLines(path, SettingsFile);
                            return;
                        }
                        i++;
                    }
                    break;
                case "SC2":
                    path = Path.Combine(Settings.Default.SC2_Location, "config.ini");
                    SettingsFile = File.ReadAllLines(path);
                    i = 0;
                    foreach(var line in SettingsFile)
                    {
                        if(line.StartsWith("PlayerName=Unreal"))
                        {
                            newLine = line.Replace("Unreal", Settings.Default.Username);
                            SettingsFile[i] = newLine;
                            File.WriteAllLines(path, SettingsFile);
                            return;
                        }
                        i++;
                    }
                    break;
                case "Shootmania":
                    path = Path.Combine(Settings.Default.Shootmania_Location, "Nadeo.ini");
                    SettingsFile = File.ReadAllLines(path);
                    i = 0;
                    foreach(var line in SettingsFile)
                    {
                        if(line.StartsWith("D:\\Games\\LAN\\ManiaPlanet"))
                        {
                            newLine = line.Replace("D:\\Games\\LAN\\ManiaPlanet", Settings.Default.Shootmania_Location);
                            SettingsFile[i] = newLine;
                            File.WriteAllLines(path, SettingsFile);
                        }
                        i++;
                    }
                    break;
                case "SWJK2":
                    path = Path.Combine(Settings.Default.SWJK2_Location, "GameData\\base\\jk2mpconfig.cfg");
                    SettingsFile = File.ReadAllLines(path);
                    i = 0;
                    foreach(var line in SettingsFile)
                    {
                        if(line.StartsWith("seta name \"Unreal\""))
                        {
                            newLine = line.Replace("Unreal", Settings.Default.Username);
                            SettingsFile[i] = newLine;
                            File.WriteAllLines(path, SettingsFile);
                            return;
                        }
                        i++;
                    }
                    break;
                case "L4D2":
                    path = Path.Combine(Settings.Default.L4D2_Location, "rev.ini");
                    SettingsFile = File.ReadAllLines(path);
                    i = 0;
                    foreach(var line in SettingsFile)
                    {
                        if(line.StartsWith("PlayerName = Unreal"))
                        {
                            newLine = line.Replace("Unreal", Settings.Default.Username);
                            SettingsFile[i] = newLine;
                            File.WriteAllLines(path, SettingsFile);
                            return;
                        }
                        i++;
                    }
                    break;
                case "UT3":
                    new WebClient().DownloadFile(new Uri($"http://{Settings.Default.ServerIP}/logiciels/launcher/UT3.zip"), @"UT3.zip");
                    using (var archive = ZipArchive.Open(Path.Combine(Environment.CurrentDirectory, "UT3.zip")))
                    {
                        foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                        {
                            entry.WriteToDirectory(Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), "Documents", "My Games", "Unreal Tournament 3"),
                                new ExtractionOptions { ExtractFullPath = true, Overwrite = true });
                        }
                    }
                    File.Delete(Path.Combine(Environment.CurrentDirectory, "UT3.zip"));
                    path = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), "Documents", "My Games\\Unreal Tournament 3\\UTGame\\Config\\UTEngine.ini");
                    SettingsFile = File.ReadAllLines(path);
                    i = 0;
                    foreach (var line in SettingsFile)
                    {
                        if (line.Equals("Name=Player"))
                        {
                            newLine = line.Replace("Player", Settings.Default.Username);
                            SettingsFile[i] = newLine;
                            File.WriteAllLines(path, SettingsFile);
                        }
                        i++;
                    }
                    path = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), "Documents", "My Games\\Unreal Tournament 3\\UTGame\\Config\\UTGame.ini");
                    SettingsFile = File.ReadAllLines(path);
                    i = 0;
                    foreach (var line in SettingsFile)
                    {
                        if (line.Equals("Name=Player"))
                        {
                            newLine = line.Replace("Player", Settings.Default.Username);
                            SettingsFile[i] = newLine;
                            File.WriteAllLines(path, SettingsFile);
                        }
                        if (line.Equals("PlayerNames="))
                        {
                            newLine = line.Replace("=", $"={Settings.Default.Username}");
                            SettingsFile[i] = newLine;
                            File.WriteAllLines(path, SettingsFile);
                        }
                        i++;
                    }
                    File.Move(
                        Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), "Documents",
                            "My Games\\Unreal Tournament 3\\UTGame\\SaveData\\Player.ue3profile"),
                        Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), "Documents",
                            $"My Games\\Unreal Tournament 3\\UTGame\\SaveData\\{Settings.Default.Username}.ue3profile"));
                    break;
                case "TF2":
                    path = Path.Combine(Settings.Default.TF2_Location, "rev.ini");
                    SettingsFile = File.ReadAllLines(path);
                    i = 0;
                    foreach(var line in SettingsFile)
                    {
                        if(line.StartsWith("PlayerName=Unreal"))
                        {
                            newLine = line.Replace("Unreal", Settings.Default.Username);
                            SettingsFile[i] = newLine;
                            File.WriteAllLines(path, SettingsFile);
                            return;
                        }
                        i++;
                    }
                    break;
            }
        }
    }
}