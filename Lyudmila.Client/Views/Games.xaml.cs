// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System.ComponentModel;
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
        private Color _ActiveColor = (Color) new ColorConverter().ConvertFrom("#F57C00");
        private SolidColorBrush _ActiveColorBrush = (SolidColorBrush) new BrushConverter().ConvertFrom("#F57C00");

        public Games()
        {
            InitializeComponent();

            ((MainWindow) Application.Current.MainWindow).SetGamesColor += _SetColor;
        }

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
    }
}