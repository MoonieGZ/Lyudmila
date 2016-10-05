// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

using Lyudmila.Client.Windows;

namespace Lyudmila.Client.Views
{
    /// <summary>
    ///   Interaction logic for Games.xaml
    /// </summary>
    public partial class Games : INotifyPropertyChanged
    {
        public Games()
        {
            InitializeComponent();

            ((MainWindow) Application.Current.MainWindow).SetGamesColor += _SetColor;
        }

        #region buttons

        private void BtnClick_More(object sender, RoutedEventArgs e)
        {
            Process.Start("http://wiki");
        }

        #endregion

        #region color stuff

        private static SolidColorBrush _ActiveColorBrush = (SolidColorBrush)Application.Current.Resources["AccentColorBrush2"];
        private Color _ActiveColor = BrushToDrawingColor(_ActiveColorBrush);

        public Color ActiveColor
        {
            get { return _ActiveColor; }
            set
            {
                _ActiveColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ActiveColor"));
            }
        }

        public SolidColorBrush ActiveColorBrush
        {
            get { return _ActiveColorBrush; }
            set
            {
                _ActiveColorBrush = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ActiveColorBrush"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void _SetColor(SolidColorBrush value)
        {
            ActiveColorBrush = value;
            ActiveColor = BrushToDrawingColor(value);
        }

        private static Color BrushToDrawingColor(SolidColorBrush br)
        {
            return Color.FromArgb(br.Color.A, br.Color.R, br.Color.G, br.Color.B);
        }

        #endregion
    }
}