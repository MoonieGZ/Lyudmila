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
                            Properties.Settings.Default.AoE2HD_Installed = true;
                            Properties.Settings.Default.AoE2HD_Location = Path.Combine(Environment.CurrentDirectory, "Jeux", "AoE2HD");
                            UsernameUpdater.SetName("AoE2HD");
                            break;
                        case "Battlefield 3":
                            new Download("BF3", $"http://{Properties.Settings.Default.ServerIP}/jeux/BF3.zip").ShowDialog();
                            Properties.Settings.Default.BF3_Installed = true;
                            Properties.Settings.Default.BF3_Location = Path.Combine(Environment.CurrentDirectory, "Jeux", "BF3");
                            UsernameUpdater.SetName("BF3");
                            break;
                        case "Blur":
                            new Download("Blur", $"http://{Properties.Settings.Default.ServerIP}/jeux/Blur.zip").ShowDialog();
                            Properties.Settings.Default.Blur_Installed = true;
                            Properties.Settings.Default.Blur_Location = Path.Combine(Environment.CurrentDirectory, "Jeux", "Blur");
                            UsernameUpdater.SetName("Blur");
                            break;
                        case "CoD2":
                            new Download("CoD2", $"http://{Properties.Settings.Default.ServerIP}/jeux/Blur.zip").ShowDialog();
                            Properties.Settings.Default.CoD2_Installed = true;
                            Properties.Settings.Default.CoD2_Location = Path.Combine(Environment.CurrentDirectory, "Jeux", "CoD2");
                            UsernameUpdater.SetName("CoD2");
                            break;
                        case "CoD4":
                            new Download("CoD4", $"http://{Properties.Settings.Default.ServerIP}/jeux/CoD4.zip").ShowDialog();
                            Properties.Settings.Default.CoD4_Installed = true;
                            Properties.Settings.Default.CoD4_Location = Path.Combine(Environment.CurrentDirectory, "Jeux", "CoD4");
                            UsernameUpdater.SetName("CoD4");
                            break;
                        case "CoD5":
                            new Download("CoD5", $"http://{Properties.Settings.Default.ServerIP}/jeux/CoD5.zip").ShowDialog();
                            Properties.Settings.Default.CoD5_Installed = true;
                            Properties.Settings.Default.CoD5_Location = Path.Combine(Environment.CurrentDirectory, "Jeux", "CoD5");
                            UsernameUpdater.SetName("CoD5");
                            break;
                    }

                    Properties.Settings.Default.Save();
                    InstallPlayIcon.Kind = PackIconKind.Play;
                    break;
                case PackIconKind.Play:
                    switch(((MainWindow) Application.Current.MainWindow).GameInstall.Header)
                    {
                        case "Age of Empires II HD":
                            var game = new ProcessStartInfo
                            {
                                WorkingDirectory = Path.Combine(Properties.Settings.Default.AoE2HD_Location),
                                FileName = Path.Combine(Properties.Settings.Default.AoE2HD_Location, "SmartSteamLoader.exe")
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