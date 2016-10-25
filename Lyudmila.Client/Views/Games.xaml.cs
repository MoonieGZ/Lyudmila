// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
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

        private async void AoE2HD_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow) Application.Current.MainWindow).GameInstall.Header = "Age of Empires II HD";
            var FadeThread = new Thread(Fade);
            FadeThread.Start();
            do
            {
                await Task.Delay(50);
            }
            while(FadeThread.IsAlive);
            ((MainWindow) Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private async void BF3_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow) Application.Current.MainWindow).GameInstall.Header = "Battlefield 3";
            var FadeThread = new Thread(Fade);
            FadeThread.Start();
            do
            {
                await Task.Delay(50);
            }
            while(FadeThread.IsAlive);
            ((MainWindow) Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private async void Blur_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow) Application.Current.MainWindow).GameInstall.Header = "Blur";
            var FadeThread = new Thread(Fade);
            FadeThread.Start();
            do
            {
                await Task.Delay(50);
            }
            while(FadeThread.IsAlive);
            ((MainWindow) Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private async void CoD2_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow) Application.Current.MainWindow).GameInstall.Header = "Call of Duty 2";
            var FadeThread = new Thread(Fade);
            FadeThread.Start();
            do
            {
                await Task.Delay(50);
            }
            while(FadeThread.IsAlive);
            ((MainWindow) Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private async void CoD4_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow) Application.Current.MainWindow).GameInstall.Header = "Call of Duty 4";
            var FadeThread = new Thread(Fade);
            FadeThread.Start();
            do
            {
                await Task.Delay(50);
            }
            while(FadeThread.IsAlive);
            ((MainWindow) Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private async void CoD5_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow) Application.Current.MainWindow).GameInstall.Header = "Call of Duty 5";
            var FadeThread = new Thread(Fade);
            FadeThread.Start();
            do
            {
                await Task.Delay(50);
            }
            while(FadeThread.IsAlive);
            ((MainWindow) Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private async void CSGO_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow) Application.Current.MainWindow).GameInstall.Header = "Counter Strike: Global Offensive";
            var FadeThread = new Thread(Fade);
            FadeThread.Start();
            do
            {
                await Task.Delay(50);
            }
            while(FadeThread.IsAlive);
            ((MainWindow) Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private async void DoDS_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow) Application.Current.MainWindow).GameInstall.Header = "Day of Defeat: Source";
            var FadeThread = new Thread(Fade);
            FadeThread.Start();
            do
            {
                await Task.Delay(50);
            }
            while(FadeThread.IsAlive);
            ((MainWindow) Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private async void DoTA2_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow) Application.Current.MainWindow).GameInstall.Header = "DoTA 2";
            var FadeThread = new Thread(Fade);
            FadeThread.Start();
            do
            {
                await Task.Delay(50);
            }
            while(FadeThread.IsAlive);
            ((MainWindow) Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private async void Flatout2_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow) Application.Current.MainWindow).GameInstall.Header = "Flatout 2";
            var FadeThread = new Thread(Fade);
            FadeThread.Start();
            do
            {
                await Task.Delay(50);
            }
            while(FadeThread.IsAlive);
            ((MainWindow) Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private async void L4D2_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow) Application.Current.MainWindow).GameInstall.Header = "Left 4 Dead 2";
            var FadeThread = new Thread(Fade);
            FadeThread.Start();
            do
            {
                await Task.Delay(50);
            }
            while(FadeThread.IsAlive);
            ((MainWindow) Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private async void SC2_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow) Application.Current.MainWindow).GameInstall.Header = "StarCraft 2";
            var FadeThread = new Thread(Fade);
            FadeThread.Start();
            do
            {
                await Task.Delay(50);
            }
            while(FadeThread.IsAlive);
            ((MainWindow) Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private async void Shootmania_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow) Application.Current.MainWindow).GameInstall.Header = "Shootmania";
            var FadeThread = new Thread(Fade);
            FadeThread.Start();
            do
            {
                await Task.Delay(50);
            }
            while(FadeThread.IsAlive);
            ((MainWindow) Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private async void SWJK2_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow) Application.Current.MainWindow).GameInstall.Header = "Star Wars: Jedi Knight 2";
            var FadeThread = new Thread(Fade);
            FadeThread.Start();
            do
            {
                await Task.Delay(50);
            }
            while(FadeThread.IsAlive);
            ((MainWindow) Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private async void TF2_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow) Application.Current.MainWindow).GameInstall.Header = "Team Fortress 2";
            var FadeThread = new Thread(Fade);
            FadeThread.Start();
            do
            {
                await Task.Delay(50);
            }
            while(FadeThread.IsAlive);
            ((MainWindow) Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private async void UT3_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow) Application.Current.MainWindow).GameInstall.Header = "Unreal Tournament 3";

            var FadeThread = new Thread(Fade);
            FadeThread.Start();
            do
            {
                await Task.Delay(50);
            }
            while(FadeThread.IsAlive);

            ((MainWindow) Application.Current.MainWindow).GameInstall.IsOpen = true;
        }

        private void Fade()
        {
            Dispatcher.Invoke(() => { ((MainWindow) Application.Current.MainWindow).LoadingReadyVisibility = Visibility.Visible; });
            var i = 0;
            do
            {
                Dispatcher.Invoke(() =>
                {
                    ((MainWindow) Application.Current.MainWindow).DialogOpacity = (float) (((MainWindow) Application.Current.MainWindow).DialogOpacity - .075);
                    ((MainWindow) Application.Current.MainWindow).LoaderOpacity = (float) (((MainWindow) Application.Current.MainWindow).LoaderOpacity + .1);
                });
                i++;
                Thread.Sleep(10);
            }
            while(i != 10);

            Thread.Sleep(800);

            i = 0;
            do
            {
                Dispatcher.Invoke(() =>
                {
                    ((MainWindow) Application.Current.MainWindow).DialogOpacity = (float) (((MainWindow) Application.Current.MainWindow).DialogOpacity + .075);
                    ((MainWindow) Application.Current.MainWindow).LoaderOpacity = (float) (((MainWindow) Application.Current.MainWindow).LoaderOpacity - .1);
                });
                i++;
                Thread.Sleep(10);
            }
            while(i != 10);

            Dispatcher.Invoke(() => { ((MainWindow) Application.Current.MainWindow).LoadingReadyVisibility = Visibility.Collapsed; });
        }

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
    }
}