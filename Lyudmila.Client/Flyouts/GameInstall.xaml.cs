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

using MahApps.Metro.Controls.Dialogs;

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
            throw new NotImplementedException();
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
                            InstallPlayIcon.Kind = PackIconKind.Play;
                        }
                        else
                        {
                            InstallLocation = "Non installé";
                            InstallPlayIcon.Kind = PackIconKind.Download;
                        }
                        break;
                }
            }
        }
    }
}