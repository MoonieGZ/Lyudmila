// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;

using Lyudmila.Client.Helpers;
using Lyudmila.Client.Windows;

using MaterialDesignThemes.Wpf;

namespace Lyudmila.Client.Flyouts
{
    /// <summary>
    ///   Interaction logic for GameInstall.xaml
    /// </summary>
    public partial class GameInstall : INotifyPropertyChanged
    {
        private string _activeImage;
        private string _description;
        private string _installLocation;

        private bool _isInstalled;

        public GameInstall()
        {
            InitializeComponent();
        }

        public string InstallLocation
        {
            get { return _installLocation; }
            set
            {
                _installLocation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("InstallLocation"));
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Description"));
            }
        }

        public string ActiveImage
        {
            get { return _activeImage; }
            set
            {
                _activeImage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ActiveImage"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            switch(InstallPlayIcon.Kind)
            {
                case PackIconKind.Download:
                    switch(((MainWindow) Application.Current.MainWindow).GameInstall.Header)
                    {
                        case "Age of Empires II HD":
                            new Download("AoE2HD", $"http://{Properties.Settings.Default.ServerIP}/jeux/AoE2HD.zip").ShowDialog();
                            UsernameUpdater.SetName("AoE2HD");
                            InstallLocation = Properties.Settings.Default.AoE2HD_Location;
                            break;
                        case "Battlefield 3":
                            new Download("BF3", $"http://{Properties.Settings.Default.ServerIP}/jeux/BF3.zip").ShowDialog();
                            UsernameUpdater.SetName("BF3");
                            InstallLocation = Properties.Settings.Default.BF3_Location;
                            break;
                        case "Blur":
                            new Download("Blur", $"http://{Properties.Settings.Default.ServerIP}/jeux/Blur.zip").ShowDialog();
                            UsernameUpdater.SetName("Blur");
                            InstallLocation = Properties.Settings.Default.Blur_Location;
                            break;
                        case "Call of Duty 2":
                            new Download("CoD2", $"http://{Properties.Settings.Default.ServerIP}/jeux/CoD2.zip").ShowDialog();
                            UsernameUpdater.SetName("CoD2");
                            InstallLocation = Properties.Settings.Default.CoD2_Location;
                            break;
                        case "Call of Duty 4":
                            new Download("CoD4", $"http://{Properties.Settings.Default.ServerIP}/jeux/CoD4.zip").ShowDialog();
                            UsernameUpdater.SetName("CoD4");
                            InstallLocation = Properties.Settings.Default.CoD4_Location;
                            break;
                        case "Call of Duty 5":
                            new Download("CoD5", $"http://{Properties.Settings.Default.ServerIP}/jeux/CoD5.zip").ShowDialog();
                            UsernameUpdater.SetName("CoD5");
                            InstallLocation = Properties.Settings.Default.CoD5_Location;
                            break;
                        case "Counter Strike: Global Offensive":
                            new Download("CSGO", $"http://{Properties.Settings.Default.ServerIP}/jeux/CSGO.zip").ShowDialog();
                            UsernameUpdater.SetName("CSGO");
                            InstallLocation = Properties.Settings.Default.CSGO_Location;
                            break;
                        case "DoTA 2":
                            new Download("DoTA2", $"http://{Properties.Settings.Default.ServerIP}/jeux/DoTA2.zip").ShowDialog();
                            UsernameUpdater.SetName("DoTA2");
                            InstallLocation = Properties.Settings.Default.DoTA2_Location;
                            break;
                        case "Day of Defeat: Source":
                            new Download("DoDS", $"http://{Properties.Settings.Default.ServerIP}/jeux/DoDS.zip").ShowDialog();
                            UsernameUpdater.SetName("DoDS");
                            InstallLocation = Properties.Settings.Default.DoDS_Location;
                            break;
                        case "Flatout 2":
                            new Download("Flatout2", $"http://{Properties.Settings.Default.ServerIP}/jeux/Flatout2.zip").ShowDialog();
                            UsernameUpdater.SetName("Flatout2");
                            InstallLocation = Properties.Settings.Default.F2_Location;
                            break;
                        case "Left 4 Dead 2":
                            new Download("L4D2", $"http://{Properties.Settings.Default.ServerIP}/jeux/L4D2.zip").ShowDialog();
                            UsernameUpdater.SetName("L4D2");
                            InstallLocation = Properties.Settings.Default.L4D2_Location;
                            break;
                        case "StarCraft 2":
                            new Download("SC2", $"http://{Properties.Settings.Default.ServerIP}/jeux/SC2.zip").ShowDialog();
                            UsernameUpdater.SetName("SC2");
                            InstallLocation = Properties.Settings.Default.SC2_Location;
                            break;
                        case "Shootmania":
                            new Download("Shootmania", $"http://{Properties.Settings.Default.ServerIP}/jeux/Shootmania.zip").ShowDialog();
                            UsernameUpdater.SetName("Shootmania");
                            InstallLocation = Properties.Settings.Default.Shootmania_Location;
                            break;
                        case "Star Wars: Jedi Knight 2":
                            new Download("SWJK2", $"http://{Properties.Settings.Default.ServerIP}/jeux/SWJK2.zip").ShowDialog();
                            UsernameUpdater.SetName("SWJK2");
                            InstallLocation = Properties.Settings.Default.SWJK2_Location;
                            break;
                        case "Team Fortress 2":
                            new Download("TF2", $"http://{Properties.Settings.Default.ServerIP}/jeux/TF2.zip").ShowDialog();
                            UsernameUpdater.SetName("TF2");
                            InstallLocation = Properties.Settings.Default.TF2_Location;
                            break;
                        case "Unreal Tournament 3":
                            new Download("UT3", $"http://{Properties.Settings.Default.ServerIP}/jeux/UT3.zip").ShowDialog();
                            UsernameUpdater.SetName("UT3");
                            InstallLocation = Properties.Settings.Default.UT3_Location;
                            break;
                    }

                    Properties.Settings.Default.Save();
                    InstallPlayIcon.Kind = PackIconKind.Play;
                    break;
                case PackIconKind.Play:
                    var game = new ProcessStartInfo();

                    switch(((MainWindow) Application.Current.MainWindow).GameInstall.Header)
                    {
                        case "Age of Empires II HD":
                            game = new ProcessStartInfo
                            {
                                WorkingDirectory = Path.Combine(Properties.Settings.Default.AoE2HD_Location),
                                FileName = Path.Combine(Properties.Settings.Default.AoE2HD_Location, "SmartSteamLoader.exe")
                            };
                            Process.Start(game);
                            break;
                        case "Battlefield 3":
                            game = new ProcessStartInfo
                            {
                                WorkingDirectory = Properties.Settings.Default.BF3_Location,
                                FileName = Path.Combine(Properties.Settings.Default.BF3_Location, "_StartGame.bat")
                            };
                            Process.Start(game);
                            break;
                        case "Blur":
                            game = new ProcessStartInfo
                            {
                                WorkingDirectory = Properties.Settings.Default.Blur_Location,
                                FileName = Path.Combine(Properties.Settings.Default.Blur_Location, "BLUR_LAUNCHER.bat")
                            };
                            Process.Start(game);
                            break;
                        case "Call of Duty 2":
                            game = new ProcessStartInfo
                            {
                                WorkingDirectory = Properties.Settings.Default.CoD2_Location,
                                FileName = Path.Combine(Properties.Settings.Default.CoD2_Location, "CoD2MP_s.exe")
                            };
                            Process.Start(game);
                            break;
                        case "Call of Duty 4":
                            game = new ProcessStartInfo
                            {
                                WorkingDirectory = Properties.Settings.Default.CoD4_Location,
                                FileName = Path.Combine(Properties.Settings.Default.CoD4_Location, "iw3mp.exe")
                            };
                            Process.Start(game);
                            break;
                        case "Call of Duty 5":
                            game = new ProcessStartInfo
                            {
                                WorkingDirectory = Properties.Settings.Default.CoD5_Location,
                                FileName = Path.Combine(Properties.Settings.Default.CoD5_Location, "CoDWaW LanFixed.exe")
                            };
                            Process.Start(game);
                            break;
                        case "Counter Strike: Global Offensive":
                            game = new ProcessStartInfo
                            {
                                WorkingDirectory = Properties.Settings.Default.CSGO_Location,
                                FileName = Path.Combine(Properties.Settings.Default.CSGO_Location, "revLoader.exe")
                            };
                            Process.Start(game);
                            break;
                        case "DoTA 2":
                            game = new ProcessStartInfo
                            {
                                WorkingDirectory = Properties.Settings.Default.DoTA2_Location,
                                FileName = Path.Combine(Properties.Settings.Default.DoTA2_Location, "revLoader.exe")
                            };
                            Process.Start(game);
                            break;
                        case "Day of Defeat: Source":
                            game = new ProcessStartInfo
                            {
                                WorkingDirectory = Properties.Settings.Default.DoDS_Location,
                                FileName = Path.Combine(Properties.Settings.Default.DoDS_Location, "Day_of_Defeat_Source.exe")
                            };
                            Process.Start(game);
                            break;
                        case "Flatout 2":
                            game = new ProcessStartInfo
                            {
                                WorkingDirectory = Properties.Settings.Default.F2_Location,
                                FileName = Path.Combine(Properties.Settings.Default.F2_Location, "flatout2multi.exe")
                            };
                            Process.Start(game);
                            break;
                        case "Left 4 Dead 2":
                            game = new ProcessStartInfo
                            {
                                WorkingDirectory = Properties.Settings.Default.L4D2_Location,
                                FileName = Path.Combine(Properties.Settings.Default.L4D2_Location, "left4dead2.exe")
                            };
                            Process.Start(game);
                            break;
                        case "StarCraft 2":
                            game = new ProcessStartInfo
                            {
                                WorkingDirectory = Properties.Settings.Default.SC2_Location,
                                FileName = Path.Combine(Properties.Settings.Default.SC2_Location, "StarFriend_Client.exe")
                            };
                            Process.Start(game);
                            break;
                        case "Shootmania":
                            game = new ProcessStartInfo
                            {
                                WorkingDirectory = Properties.Settings.Default.Shootmania_Location,
                                FileName = Path.Combine(Properties.Settings.Default.Shootmania_Location, "ManiaPlanetLauncher.exe")
                            };
                            Process.Start(game);
                            break;
                        case "Star Wars: Jedi Knight 2":
                            game = new ProcessStartInfo
                            {
                                WorkingDirectory = Path.Combine(Properties.Settings.Default.SWJK2_Location, "GameData"),
                                FileName = Path.Combine(Properties.Settings.Default.SWJK2_Location, "GameData", "jk2mp.exe")
                            };
                            Process.Start(game);
                            break;
                        case "Team Fortress 2":
                            game = new ProcessStartInfo
                            {
                                WorkingDirectory = Properties.Settings.Default.TF2_Location,
                                FileName = Path.Combine(Properties.Settings.Default.TF2_Location, "revLoader.exe")
                            };
                            Process.Start(game);
                            break;
                        case "Unreal Tournament 3":
                            game = new ProcessStartInfo
                            {
                                WorkingDirectory = Path.Combine(Properties.Settings.Default.UT3_Location, "Binaries"),
                                FileName = Path.Combine(Properties.Settings.Default.UT3_Location, "Binaries", "UT3.exe")
                            };
                            Process.Start(game);
                            break;
                    }
                    break;
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            if(_isInstalled)
            {
                if(Directory.Exists(InstallLocation))
                {
                    Process.Start(InstallLocation);
                }
            }
            else
            {
                var dialog = new Location(((MainWindow) Application.Current.MainWindow).GameInstall.Header);
                var showDialog = dialog.ShowDialog();
                if(showDialog != null && !showDialog.Value)
                {
                    InstallLocation = dialog.GameLocation;
                    InstallPlayIcon.Kind = PackIconKind.Play;
                }
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if((bool) e.NewValue)
            {
                switch(((MainWindow) Application.Current.MainWindow).GameInstall.Header)
                {
                    case "Age of Empires II HD":
                        ActiveImage = "pack://application:,,,/Resources/img/AoE2HD.jpg";
                        Description = GameDescriptions.AoE2HD;
                        if(Properties.Settings.Default.AoE2HD_Installed)
                        {
                            InstallLocation = Properties.Settings.Default.AoE2HD_Location;
                            _isInstalled = true;
                            InstallPlayIcon.Kind = PackIconKind.Play;
                        }
                        else
                        {
                            InstallLocation = "Non installé";
                            _isInstalled = false;
                            InstallPlayIcon.Kind = PackIconKind.Download;
                        }
                        break;
                    case "Battlefield 3":
                        ActiveImage = "pack://application:,,,/Resources/img/BF3.jpg";
                        Description = GameDescriptions.BF3;
                        if(Properties.Settings.Default.BF3_Installed)
                        {
                            InstallLocation = Properties.Settings.Default.BF3_Location;
                            _isInstalled = true;
                            InstallPlayIcon.Kind = PackIconKind.Play;
                        }
                        else
                        {
                            InstallLocation = "Non installé";
                            _isInstalled = false;
                            InstallPlayIcon.Kind = PackIconKind.Download;
                        }
                        break;
                    case "Blur":
                        ActiveImage = "pack://application:,,,/Resources/img/Blur.jpg";
                        Description = GameDescriptions.Blur;
                        if(Properties.Settings.Default.Blur_Installed)
                        {
                            InstallLocation = Properties.Settings.Default.Blur_Location;
                            _isInstalled = true;
                            InstallPlayIcon.Kind = PackIconKind.Play;
                        }
                        else
                        {
                            InstallLocation = "Non installé";
                            _isInstalled = false;
                            InstallPlayIcon.Kind = PackIconKind.Download;
                        }
                        break;
                    case "Call of Duty 2":
                        ActiveImage = "pack://application:,,,/Resources/img/CoD2.jpg";
                        Description = GameDescriptions.CoD2;
                        if(Properties.Settings.Default.CoD2_Installed)
                        {
                            InstallLocation = Properties.Settings.Default.CoD2_Location;
                            _isInstalled = true;
                            InstallPlayIcon.Kind = PackIconKind.Play;
                        }
                        else
                        {
                            InstallLocation = "Non installé";
                            _isInstalled = false;
                            InstallPlayIcon.Kind = PackIconKind.Download;
                        }
                        break;
                    case "Call of Duty 4":
                        ActiveImage = "pack://application:,,,/Resources/img/CoD4.jpg";
                        Description = GameDescriptions.CoD4;
                        if(Properties.Settings.Default.CoD4_Installed)
                        {
                            InstallLocation = Properties.Settings.Default.CoD4_Location;
                            _isInstalled = true;
                            InstallPlayIcon.Kind = PackIconKind.Play;
                        }
                        else
                        {
                            InstallLocation = "Non installé";
                            _isInstalled = false;
                            InstallPlayIcon.Kind = PackIconKind.Download;
                        }
                        break;
                    case "Call of Duty 5":
                        ActiveImage = "pack://application:,,,/Resources/img/CoD5.jpg";
                        Description = GameDescriptions.CoD5;
                        if(Properties.Settings.Default.CoD5_Installed)
                        {
                            InstallLocation = Properties.Settings.Default.CoD5_Location;
                            _isInstalled = true;
                            InstallPlayIcon.Kind = PackIconKind.Play;
                        }
                        else
                        {
                            InstallLocation = "Non installé";
                            _isInstalled = false;
                            InstallPlayIcon.Kind = PackIconKind.Download;
                        }
                        break;
                    case "Counter Strike: Global Offensive":
                        ActiveImage = "pack://application:,,,/Resources/img/CSGO.jpg";
                        Description = GameDescriptions.CSGO;
                        if(Properties.Settings.Default.CSGO_Installed)
                        {
                            InstallLocation = Properties.Settings.Default.CSGO_Location;
                            _isInstalled = true;
                            InstallPlayIcon.Kind = PackIconKind.Play;
                        }
                        else
                        {
                            InstallLocation = "Non installé";
                            _isInstalled = false;
                            InstallPlayIcon.Kind = PackIconKind.Download;
                        }
                        break;
                    case "DoTA 2":
                        ActiveImage = "pack://application:,,,/Resources/img/D2.jpg";
                        Description = GameDescriptions.D2;
                        if(Properties.Settings.Default.DoTA2_Installed)
                        {
                            InstallLocation = Properties.Settings.Default.DoTA2_Location;
                            _isInstalled = true;
                            InstallPlayIcon.Kind = PackIconKind.Play;
                        }
                        else
                        {
                            InstallLocation = "Non installé";
                            _isInstalled = false;
                            InstallPlayIcon.Kind = PackIconKind.Download;
                        }
                        break;
                    case "Day of Defeat: Source":
                        ActiveImage = "pack://application:,,,/Resources/img/DoDS.jpg";
                        Description = GameDescriptions.DoDS;
                        if(Properties.Settings.Default.DoDS_Installed)
                        {
                            InstallLocation = Properties.Settings.Default.DoDS_Location;
                            _isInstalled = true;
                            InstallPlayIcon.Kind = PackIconKind.Play;
                        }
                        else
                        {
                            InstallLocation = "Non installé";
                            _isInstalled = false;
                            InstallPlayIcon.Kind = PackIconKind.Download;
                        }
                        break;
                    case "Flatout 2":
                        ActiveImage = "pack://application:,,,/Resources/img/F2.jpg";
                        Description = GameDescriptions.F2;
                        if(Properties.Settings.Default.F2_Installed)
                        {
                            InstallLocation = Properties.Settings.Default.F2_Location;
                            _isInstalled = true;
                            InstallPlayIcon.Kind = PackIconKind.Play;
                        }
                        else
                        {
                            InstallLocation = "Non installé";
                            _isInstalled = false;
                            InstallPlayIcon.Kind = PackIconKind.Download;
                        }
                        break;
                    case "Left 4 Dead 2":
                        ActiveImage = "pack://application:,,,/Resources/img/L4D2.jpg";
                        Description = GameDescriptions.L4D2;
                        if(Properties.Settings.Default.L4D2_Installed)
                        {
                            InstallLocation = Properties.Settings.Default.L4D2_Location;
                            _isInstalled = true;
                            InstallPlayIcon.Kind = PackIconKind.Play;
                        }
                        else
                        {
                            InstallLocation = "Non installé";
                            _isInstalled = false;
                            InstallPlayIcon.Kind = PackIconKind.Download;
                        }
                        break;
                    case "StarCraft 2":
                        ActiveImage = "pack://application:,,,/Resources/img/SC2.jpg";
                        Description = GameDescriptions.SC2;
                        if(Properties.Settings.Default.SC2_Installed)
                        {
                            InstallLocation = Properties.Settings.Default.SC2_Location;
                            _isInstalled = true;
                            InstallPlayIcon.Kind = PackIconKind.Play;
                        }
                        else
                        {
                            InstallLocation = "Non installé";
                            _isInstalled = false;
                            InstallPlayIcon.Kind = PackIconKind.Download;
                        }
                        break;
                    case "Shootmania":
                        ActiveImage = "pack://application:,,,/Resources/img/Shootmania.jpg";
                        Description = GameDescriptions.Shootmania;
                        if(Properties.Settings.Default.Shootmania_Installed)
                        {
                            InstallLocation = Properties.Settings.Default.Shootmania_Location;
                            _isInstalled = true;
                            InstallPlayIcon.Kind = PackIconKind.Play;
                        }
                        else
                        {
                            InstallLocation = "Non installé";
                            _isInstalled = false;
                            InstallPlayIcon.Kind = PackIconKind.Download;
                        }
                        break;
                    case "Star Wars: Jedi Knight 2":
                        ActiveImage = "pack://application:,,,/Resources/img/SWJK2.jpg";
                        Description = GameDescriptions.SWJK2;
                        if(Properties.Settings.Default.SWJK2_Installed)
                        {
                            InstallLocation = Properties.Settings.Default.SWJK2_Location;
                            _isInstalled = true;
                            InstallPlayIcon.Kind = PackIconKind.Play;
                        }
                        else
                        {
                            InstallLocation = "Non installé";
                            _isInstalled = false;
                            InstallPlayIcon.Kind = PackIconKind.Download;
                        }
                        break;
                    case "Team Fortress 2":
                        ActiveImage = "pack://application:,,,/Resources/img/TF2.jpg";
                        Description = GameDescriptions.TF2;
                        if(Properties.Settings.Default.TF2_Installed)
                        {
                            InstallLocation = Properties.Settings.Default.TF2_Location;
                            _isInstalled = true;
                            InstallPlayIcon.Kind = PackIconKind.Play;
                        }
                        else
                        {
                            InstallLocation = "Non installé";
                            _isInstalled = false;
                            InstallPlayIcon.Kind = PackIconKind.Download;
                        }
                        break;
                    case "Unreal Tournament 3":
                        ActiveImage = "pack://application:,,,/Resources/img/UT3.jpg";
                        Description = GameDescriptions.UT3;
                        if(Properties.Settings.Default.UT3_Installed)
                        {
                            InstallLocation = Properties.Settings.Default.UT3_Location;
                            _isInstalled = true;
                            InstallPlayIcon.Kind = PackIconKind.Play;
                        }
                        else
                        {
                            InstallLocation = "Non installé";
                            _isInstalled = false;
                            InstallPlayIcon.Kind = PackIconKind.Download;
                        }
                        break;
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            switch(((MainWindow) Application.Current.MainWindow).GameInstall.Header)
            {
                case "Age of Empires II HD":
                    Delete("AoE2HD");
                    if (Properties.Settings.Default.AoE2HD_Installed)
                    {
                        Properties.Settings.Default.AoE2HD_Location = string.Empty;
                        Properties.Settings.Default.AoE2HD_Installed = false;
                        Properties.Settings.Default.Save();
                        InstallLocation = "Non installé";
                        _isInstalled = false;
                        InstallPlayIcon.Kind = PackIconKind.Download;
                    }
                    break;
                case "Battlefield 3":
                    Delete("BF3");
                    if (Properties.Settings.Default.BF3_Installed)
                    {
                        Properties.Settings.Default.BF3_Location = string.Empty;
                        Properties.Settings.Default.BF3_Installed = false;
                        Properties.Settings.Default.Save();
                        InstallLocation = "Non installé";
                        _isInstalled = false;
                        InstallPlayIcon.Kind = PackIconKind.Download;
                    }
                    break;
                case "Blur":
                    Delete("Blur");
                    if (Properties.Settings.Default.Blur_Installed)
                    {
                        Properties.Settings.Default.Blur_Location = string.Empty;
                        Properties.Settings.Default.Blur_Installed = false;
                        Properties.Settings.Default.Save();
                        InstallLocation = "Non installé";
                        _isInstalled = false;
                        InstallPlayIcon.Kind = PackIconKind.Download;
                    }
                    break;
                case "Call of Duty 2":
                    Delete("CoD2");
                    if (Properties.Settings.Default.CoD2_Installed)
                    {
                        Properties.Settings.Default.CoD2_Location = string.Empty;
                        Properties.Settings.Default.CoD2_Installed = false;
                        Properties.Settings.Default.Save();
                        InstallLocation = "Non installé";
                        _isInstalled = false;
                        InstallPlayIcon.Kind = PackIconKind.Download;
                    }
                    break;
                case "Call of Duty 4":
                    Delete("CoD4");
                    if (Properties.Settings.Default.CoD4_Installed)
                    {
                        Properties.Settings.Default.CoD4_Location = string.Empty;
                        Properties.Settings.Default.CoD4_Installed = false;
                        Properties.Settings.Default.Save();
                        InstallLocation = "Non installé";
                        _isInstalled = false;
                        InstallPlayIcon.Kind = PackIconKind.Download;
                    }
                    break;
                case "Call of Duty 5":
                    Delete("CoD5");
                    if (Properties.Settings.Default.CoD5_Installed)
                    {
                        Properties.Settings.Default.CoD5_Location = string.Empty;
                        Properties.Settings.Default.CoD5_Installed = false;
                        Properties.Settings.Default.Save();
                        InstallLocation = "Non installé";
                        _isInstalled = false;
                        InstallPlayIcon.Kind = PackIconKind.Download;
                    }
                    break;
                case "Counter Strike: Global Offensive":
                    Delete("CSGO");
                    if (Properties.Settings.Default.CSGO_Installed)
                    {
                        Properties.Settings.Default.CSGO_Location = string.Empty;
                        Properties.Settings.Default.CSGO_Installed = false;
                        Properties.Settings.Default.Save();
                        InstallLocation = "Non installé";
                        _isInstalled = false;
                        InstallPlayIcon.Kind = PackIconKind.Download;
                    }
                    break;
                case "DoTA 2":
                    Delete("DoTA2");
                    if (Properties.Settings.Default.DoTA2_Installed)
                    {
                        Properties.Settings.Default.DoTA2_Location = string.Empty;
                        Properties.Settings.Default.DoTA2_Installed = false;
                        Properties.Settings.Default.Save();
                        InstallLocation = "Non installé";
                        _isInstalled = false;
                        InstallPlayIcon.Kind = PackIconKind.Download;
                    }
                    break;
                case "Day of Defeat: Source":
                    Delete("DoDS");
                    if (Properties.Settings.Default.DoDS_Installed)
                    {
                        Properties.Settings.Default.DoDS_Location = string.Empty;
                        Properties.Settings.Default.DoDS_Installed = false;
                        Properties.Settings.Default.Save();
                        InstallLocation = "Non installé";
                        _isInstalled = false;
                        InstallPlayIcon.Kind = PackIconKind.Download;
                    }
                    break;
                case "Flatout 2":
                    Delete("F2");
                    if (Properties.Settings.Default.F2_Installed)
                    {
                        Properties.Settings.Default.F2_Location = string.Empty;
                        Properties.Settings.Default.F2_Installed = false;
                        Properties.Settings.Default.Save();
                        InstallLocation = "Non installé";
                        _isInstalled = false;
                        InstallPlayIcon.Kind = PackIconKind.Download;
                    }
                    break;
                case "Left 4 Dead 2":
                    Delete("L4D2");
                    if (Properties.Settings.Default.L4D2_Installed)
                    {
                        Properties.Settings.Default.L4D2_Location = string.Empty;
                        Properties.Settings.Default.L4D2_Installed = false;
                        Properties.Settings.Default.Save();
                        InstallLocation = "Non installé";
                        _isInstalled = false;
                        InstallPlayIcon.Kind = PackIconKind.Download;
                    }
                    break;
                case "StarCraft 2":
                    Delete("SC2");
                    if (Properties.Settings.Default.SC2_Installed)
                    {
                        Properties.Settings.Default.SC2_Location = string.Empty;
                        Properties.Settings.Default.SC2_Installed = false;
                        Properties.Settings.Default.Save();
                        InstallLocation = "Non installé";
                        _isInstalled = false;
                        InstallPlayIcon.Kind = PackIconKind.Download;
                    }
                    break;
                case "Shootmania":
                    Delete("Shootmania");
                    if (Properties.Settings.Default.Shootmania_Installed)
                    {
                        Properties.Settings.Default.Shootmania_Location = string.Empty;
                        Properties.Settings.Default.Shootmania_Installed = false;
                        Properties.Settings.Default.Save();
                        InstallLocation = "Non installé";
                        _isInstalled = false;
                        InstallPlayIcon.Kind = PackIconKind.Download;
                    }
                    break;
                case "Star Wars: Jedi Knight 2":
                    Delete("SWJK2");
                    if (Properties.Settings.Default.SWJK2_Installed)
                    {
                        Properties.Settings.Default.SWJK2_Location = string.Empty;
                        Properties.Settings.Default.SWJK2_Installed = false;
                        Properties.Settings.Default.Save();
                        InstallLocation = "Non installé";
                        _isInstalled = false;
                        InstallPlayIcon.Kind = PackIconKind.Download;
                    }
                    break;
                case "Team Fortress 2":
                    Delete("TF2");
                    if (Properties.Settings.Default.TF2_Installed)
                    {
                        Properties.Settings.Default.TF2_Location = string.Empty;
                        Properties.Settings.Default.TF2_Installed = false;
                        Properties.Settings.Default.Save();
                        InstallLocation = "Non installé";
                        _isInstalled = false;
                        InstallPlayIcon.Kind = PackIconKind.Download;
                    }
                    break;
                case "Unreal Tournament 3":
                    Delete("UT3");
                    if (Properties.Settings.Default.UT3_Installed)
                    {
                        Properties.Settings.Default.UT3_Location = string.Empty;
                        Properties.Settings.Default.UT3_Installed = false;
                        Properties.Settings.Default.Save();
                        InstallLocation = "Non installé";
                        _isInstalled = false;
                        InstallPlayIcon.Kind = PackIconKind.Download;
                    }
                    break;
            }
        }

        private static void Delete(string gameName)
        {
            try
            {
                Directory.Delete(Path.Combine(Environment.CurrentDirectory, "Jeux", gameName), true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType()}: {ex.Message}");
            }
        }
    }
}