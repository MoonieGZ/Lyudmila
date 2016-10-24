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
            switch(InstallPlayIcon.Kind) {
                case PackIconKind.Download:
                    switch (((MainWindow)Application.Current.MainWindow).GameInstall.Header)
                    {
                        case "Age of Empires II HD":
                            new Download("AoE2HD", $"http://{Properties.Settings.Default.ServerIP}/jeux/AoE2HD.zip").ShowDialog();
                            UsernameUpdater.SetName("AoE2HD");
                            break;
                        case "Battlefield 3":
                            new Download("BF3", $"http://{Properties.Settings.Default.ServerIP}/jeux/BF3.zip").ShowDialog();
                            UsernameUpdater.SetName("BF3");
                            break;
                        case "Blur":
                            new Download("Blur", $"http://{Properties.Settings.Default.ServerIP}/jeux/Blur.zip").ShowDialog();
                            UsernameUpdater.SetName("Blur");
                            break;
                        case "Call of Duty 2":
                            new Download("CoD2", $"http://{Properties.Settings.Default.ServerIP}/jeux/Blur.zip").ShowDialog();
                            UsernameUpdater.SetName("CoD2");
                            break;
                        case "Call of Duty 4":
                            new Download("CoD4", $"http://{Properties.Settings.Default.ServerIP}/jeux/CoD4.zip").ShowDialog();
                            UsernameUpdater.SetName("CoD4");
                            break;
                        case "Call of Duty 5":
                            new Download("CoD5", $"http://{Properties.Settings.Default.ServerIP}/jeux/CoD5.zip").ShowDialog();
                            UsernameUpdater.SetName("CoD5");
                            break;
                        case "Counter Stike: Global Offensive":
                            new Download("CSGO", $"http://{Properties.Settings.Default.ServerIP}/jeux/CSGO.zip").ShowDialog();
                            UsernameUpdater.SetName("CSGO");
                            break;
                        case "DoTA 2":
                            new Download("DoTA2", $"http://{Properties.Settings.Default.ServerIP}/jeux/DoTA2.zip").ShowDialog();
                            UsernameUpdater.SetName("DoTA2");
                            break;
                        case "Day of Defeat: Source":
                            new Download("DoDS", $"http://{Properties.Settings.Default.ServerIP}/jeux/DoDS.zip").ShowDialog();
                            UsernameUpdater.SetName("DoDS");
                            break;
                        case "Flatout 2":
                            new Download("Flatout2", $"http://{Properties.Settings.Default.ServerIP}/jeux/Flatout2.zip").ShowDialog();
                            UsernameUpdater.SetName("Flatout2");
                            break;
                        case "Left 4 Dead 2":
                            new Download("L4D2", $"http://{Properties.Settings.Default.ServerIP}/jeux/L4D2.zip").ShowDialog();
                            UsernameUpdater.SetName("L4D2");
                            break;
                        case "Starcraft 2":
                            new Download("SC2", $"http://{Properties.Settings.Default.ServerIP}/jeux/SC2.zip").ShowDialog();
                            UsernameUpdater.SetName("SC2");
                            break;
                        case "Shootmania":
                            new Download("Shootmania", $"http://{Properties.Settings.Default.ServerIP}/jeux/Shootmania.zip").ShowDialog();
                            UsernameUpdater.SetName("Shootmania");
                            break;
                        case "Star Wars: Jedi Knight 2":
                            new Download("SWJK2", $"http://{Properties.Settings.Default.ServerIP}/jeux/SWJK2.zip").ShowDialog();
                            UsernameUpdater.SetName("SWJK2");
                            break;
                        case "Team Fortress 2":
                            new Download("TF2", $"http://{Properties.Settings.Default.ServerIP}/jeux/TF2.zip").ShowDialog();
                            UsernameUpdater.SetName("TF2");
                            break;
                        case "Unreal Tournament 3":
                            new Download("UT3", $"http://{Properties.Settings.Default.ServerIP}/jeux/UT3.zip").ShowDialog();
                            UsernameUpdater.SetName("UT3");
                            break;
                    }

                    Properties.Settings.Default.Save();
                    InstallPlayIcon.Kind = PackIconKind.Play;
                    break;
                case PackIconKind.Play:
                    var game = new ProcessStartInfo();

                    switch (((MainWindow) Application.Current.MainWindow).GameInstall.Header)
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
                        case "Counter Stike: Global Offensive":
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
                        case "Starcraft 2":
                            game = new ProcessStartInfo
                            {
                                WorkingDirectory = Properties.Settings.Default.SC2_Location,
                                FileName = Path.Combine(Properties.Settings.Default.SC2_Location, "????????????.exe") // TODO
                            };
                            Process.Start(game);
                            break;
                        case "Shootmania":
                            game = new ProcessStartInfo
                            {
                                WorkingDirectory = Properties.Settings.Default.Shootmania_Location,
                                FileName = Path.Combine(Properties.Settings.Default.Shootmania_Location, "ManiaPlanetLauncher.exe") // TODO
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
                        if (Properties.Settings.Default.BF3_Installed)
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

                }
            }
        }
    }
}