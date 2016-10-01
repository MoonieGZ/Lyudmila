// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using Lyudmila.Client.Helpers;
using Lyudmila.Client.Views;

using MahApps.Metro.Controls;

namespace Lyudmila.Client.Windows
{
    /// <summary>
    ///   Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            PopulateStuff();
        }

        private void PopulateStuff()
        {
            ActivePage = "Games";
            StatusBarContent = "Hey look, it says test over here 🍂.";
            StatusBarDownloads = "No current downloads.";
            StatusBarClock = DateTime.Now.ToString("t");

            ClockUpdater.Start();
        }

        public event Action<SolidColorBrush> SetGamesColor;
        
        private void ToggleFlyout(int index)
        {
            var flyout = Flyouts.Items[index] as Flyout;
            if(flyout == null)
            {
                return;
            }

            flyout.IsOpen = !flyout.IsOpen;
        }

        #region boring binding

        public event PropertyChangedEventHandler PropertyChanged;

        private SolidColorBrush _StatusBarBrush = (SolidColorBrush) new BrushConverter().ConvertFrom("#F57C00");
        private string _StatusBarContent;
        private string _StatusBarDownloads;
        private string _StatusBarClock;

        private string _ActivePage;

        public SolidColorBrush StatusBarBrush
        {
            get { return _StatusBarBrush; }
            set
            {
                _StatusBarBrush = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StatusBarBrush"));
            }
        }

        public string ActivePage
        {
            get { return _ActivePage; }
            set
            {
                _ActivePage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ActivePage"));
            }
        }

        public string StatusBarContent
        {
            get { return _StatusBarContent; }
            set
            {
                _StatusBarContent = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StatusBarContent"));
            }
        }

        public string StatusBarDownloads
        {
            get { return _StatusBarDownloads; }
            set
            {
                _StatusBarDownloads = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StatusBarDownloads"));
            }
        }

        public string StatusBarClock
        {
            get { return _StatusBarClock; }
            set
            {
                _StatusBarClock = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StatusBarClock"));
            }
        }

        #endregion

        #region events

        private void MetroWindow_GotFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            StatusBarBrush = (SolidColorBrush) new BrushConverter().ConvertFrom("#F57C00");
            SetGamesColor(StatusBarBrush);
        }

        private void MetroWindow_LostFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            StatusBarBrush = (SolidColorBrush) new BrushConverter().ConvertFrom("#808080");
            SetGamesColor(StatusBarBrush);
        }

        private void ButtonDownloads_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("test");
        }

        private void ButtonSettings_OnClick(object sender, RoutedEventArgs e)
        {
            ToggleFlyout(0);
        }

        private void ButtonTeamSpeak_OnClick(object sender, RoutedEventArgs e)
        {
            ToggleFlyout(1);
        }

        private void ButtonNotifications_OnClick(object sender, RoutedEventArgs e)
        {
            ToggleFlyout(2);
        }

        private void ButtonFriends_OnClick(object sender, RoutedEventArgs e)
        {
            ItemsListBox.SelectedIndex = 2;
        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MenuToggleButton.IsChecked = false;
        }

        #endregion

        private void ItemsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(ItemsListBox.SelectedIndex)
            {
                case 0:
                    ActivePage = "Games";
                    break;
                case 1:
                    ActivePage = "Music";
                    break;
                case 2:
                    ActivePage = "Friends";
                    break;
            }

            if(ItemsListBox.SelectedIndex == 1)
            {
                if (!Music._ready)
                {
                    Music.Init();
                    DrawerHost.IsLeftDrawerOpen = false;
                }
            }
        }

        private void MetroWindow_Closing(object sender, CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}