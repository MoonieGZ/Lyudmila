// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System.ComponentModel;

namespace Lyudmila.Client.Flyouts
{
    /// <summary>
    ///   Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : INotifyPropertyChanged
    {
        public Settings()
        {
            InitializeComponent();
        }

        public string Nickname
        {
            get { return Properties.Settings.Default.Username; }
            set
            {
                Properties.Settings.Default.Username = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Nickname"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}