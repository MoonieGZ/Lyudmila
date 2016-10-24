// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Media;

using Lyudmila.Client.Flyouts;
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

        private static SolidColorBrush _ActiveColorBrush = (SolidColorBrush) Application.Current.Resources["AccentColorBrush2"];
        private static Color _ActiveColor = BrushToDrawingColor(_ActiveColorBrush);

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
            try
            {
                return Color.FromArgb(br.Color.A, br.Color.R, br.Color.G, br.Color.B);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"{ex.GetType()}: {ex.Message}");
                return Colors.White;
            }
            
        }

        #endregion

        private void AoE2HD_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow) Application.Current.MainWindow).GameInstall.Header = "Age of Empires II HD";
            Thread.Sleep(100);
            ((MainWindow) Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private void BF3_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).GameInstall.Header = "Battlefield 3";
            ((MainWindow)Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private void Blur_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).GameInstall.Header = "Blur";
            ((MainWindow)Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private void CoD2_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).GameInstall.Header = "Call of Duty 2";
            ((MainWindow)Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private void CoD4_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).GameInstall.Header = "Call of Duty 4";
            ((MainWindow)Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private void CoD5_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).GameInstall.Header = "Call of Duty 5";
            ((MainWindow)Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private void CSGO_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).GameInstall.Header = "Counter Strike: Global Offensive";
            ((MainWindow)Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private void DoDS_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).GameInstall.Header = "Day of Defeat: Source";
            ((MainWindow)Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private void DoTA2_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).GameInstall.Header = "DoTA 2";
            ((MainWindow)Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private void Flatout2_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).GameInstall.Header = "Flatout 2";
            ((MainWindow)Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private void L4D2_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).GameInstall.Header = "Left 4 Dead 2";
            ((MainWindow)Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private void SC2_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).GameInstall.Header = "StarCraft 2";
            ((MainWindow)Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private void Shootmania_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).GameInstall.Header = "Shootmania";
            ((MainWindow)Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private void SWJK2_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).GameInstall.Header = "Star Wars: Jedi Knight 2";
            ((MainWindow)Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private void TF2_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).GameInstall.Header = "Team Fortress 2";
            ((MainWindow)Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private void UT3_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).GameInstall.Header = "Unreal Tournament 3";
            ((MainWindow)Application.Current.MainWindow).GameInstall.IsOpen = true;
        }
    }
}