// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System.ComponentModel;
using System.Windows;

namespace Lyudmila.Client.Windows
{
    /// <summary>
    ///   Interaction logic for Location.xaml
    /// </summary>
    public partial class Location : INotifyPropertyChanged
    {
        private string _BrowseText;
        private readonly string _gameName;
        public string GameLocation;

        public Location(string gameName)
        {
            InitializeComponent();
            _gameName = gameName;
        }

        public string BrowseText
        {
            get { return _BrowseText; }
            set
            {
                _BrowseText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BrowseText"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            BrowseText = $"Localisez {_gameName}";
        }

        private void Validate_Click(object sender, RoutedEventArgs e)
        {
            GameLocation = LocationBox.Text;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            GameLocation = null;
            Close();
        }
    }
}