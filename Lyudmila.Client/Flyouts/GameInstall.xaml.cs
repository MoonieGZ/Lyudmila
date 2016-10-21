// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.ComponentModel;
using System.Windows;

using Lyudmila.Client.Windows;

namespace Lyudmila.Client.Flyouts
{
    /// <summary>
    ///   Interaction logic for GameInstall.xaml
    /// </summary>
    public partial class GameInstall : INotifyPropertyChanged
    {
        private string _activeImage;
        private string _installLocation;
        private string _description;

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
            throw new NotImplementedException();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if((bool) e.NewValue)
            {
                if(((MainWindow) Application.Current.MainWindow).GameInstall.Header.Equals("Age of Empires II HD"))
                {
                    ActiveImage = "pack://application:,,,/Resources/img/AoE2HD.jpg";
                    Description =
                        "Dans Age of Empires II: HD Edition, les fans du jeu original et les nouveaux venus vont tomber sous le charme de cette expérience authentique du classique Age of Empires II. Explorez toutes les campagnes solo des extensions Age of Kings et The Conquerors, faites votre choix parmi 18 civilisations s'étendant sur plus d'un millier d'années et affrontez d'autres joueurs pour votre quête dans la domination mondiale au fur et à mesure des âges. Développé originellement par Ensemble Studios et ré-imaginé en haute-définition par Hidden Path Entertainment.";
                    InstallLocation = "D:\\Games\\LAN\\AoE2HD";
                }
            }
        }
    }
}