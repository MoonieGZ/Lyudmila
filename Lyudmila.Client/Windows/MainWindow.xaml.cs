// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using Lyudmila.Client.Helpers;
using Lyudmila.Client.Properties;
using Lyudmila.Client.Views;

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

// ReSharper disable UnusedMember.Local

namespace Lyudmila.Client.Windows
{
    /// <summary>
    ///   Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private readonly MetroDialogSettings mDialog_settings = new MetroDialogSettings
        {
            AffirmativeButtonText = "OK",
            AnimateShow = true,
            AnimateHide = true,
            SuppressDefaultResources = true,
            CustomResourceDictionary = new ResourceDictionary {Source = new Uri("pack://application:,,,/Resources/Themes/Dialogs.xaml")}
        };

        public MainWindow()
        {
            InitializeComponent();
            PopulateStuff();
        }

        private void PopulateStuff()
        {
            ActivePage = "Games";
            NickName = string.Empty;
            StatusBarContent = string.Empty;
            StatusBarDownloads = string.Empty;
            StatusBarClock = DateTime.Now.ToString("t");
            LoadingReadyVisibility = Visibility.Collapsed;
            DialogOpacity = 1;
            LoaderOpacity = (float) 0.2;

            ClockUpdater.Start();
        }

        private static bool Verify(string nickname)
        {
            return !string.IsNullOrEmpty(nickname) && (nickname.Length <= 16) && (nickname.Length >= 4) && Regex.IsMatch(nickname, @"^[a-zA-Z0-9_\-\[\]\s]+$")
                   && !Regex.IsMatch(nickname, @"^[\s]+$") && !nickname.ToLower().Contains("admin");
        }

        public event Action<SolidColorBrush> SetGamesColor;
        public event Action<SolidColorBrush> SetMusicColor;

        private void ToggleFlyout(int index)
        {
            var flyout = Flyouts.Items[index] as Flyout;
            if(flyout == null)
            {
                return;
            }

            flyout.IsOpen = !flyout.IsOpen;
        }

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
                if(!Music._ready)
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

        private async void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(Settings.Default.Username))
            {
                await ChangeNick(true);
            }

            NickName = $"Bienvenue, {Settings.Default.Username}";
        }

        private async Task ChangeNick(bool quitOnCancel)
        {
            var valid = false;
            do
            {
                var nickname = await this.ShowInputAsync("Lyudmila", "Entrez votre pseudo:", mDialog_settings);

                if(nickname == null)
                {
                    if(!quitOnCancel)
                        return;

                    Environment.Exit(0);
                }

                if(Verify(nickname))
                {
                    valid = true;

                    Settings.Default.Username = nickname;
                    Settings.Default.Save();
                    /*
                     * NetTools.Init();
                     * do
                     * {
                     *     await Task.Delay(1000);
                     * }
                     * while(string.IsNullOrEmpty(Settings.Default.ServerIP));
                     */
                }
                else
                {
                    ShowMessage("Pseudo invalide :(");
                }
            }
            while(!valid);
        }

        public async void ShowMessage(string message)
        {
            await this.ShowMessageAsync("Lyudmila", message, MessageDialogStyle.Affirmative, mDialog_settings);
        }

        private async void ButtonAdminstration_OnClick(object sender, RoutedEventArgs e)
        {
            var m = new LoginDialogSettings
            {
                AffirmativeButtonText = "Se connecter",
                NegativeButtonText = "Annuler",
                AnimateShow = true,
                AnimateHide = true,
                SuppressDefaultResources = true,
                CustomResourceDictionary = new ResourceDictionary {Source = new Uri("pack://application:,,,/Resources/Themes/Dialogs.xaml")},
                InitialPassword = "password",
                InitialUsername = "utilisateur",
                NegativeButtonVisibility = Visibility.Visible,
                RememberCheckBoxVisibility = Visibility.Visible,
                RememberCheckBoxText = "Se souvenir de moi"
            };
            var login = await this.ShowLoginAsync("Se connecter a l'interface administrateur:", " ", m);

            if(VerifyLogin(login))
            {
                ToggleFlyout(3);
            }
        }

        private static bool VerifyLogin(LoginDialogData login)
        {
            // TODO: check login with server
            return true;
        }

        private async void NickName_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            await ChangeNick(false);

            NickName = $"Bienvenue, {Settings.Default.Username}";
        }

        #region boring binding

        public event PropertyChangedEventHandler PropertyChanged;

        private SolidColorBrush _StatusBarBrush = (SolidColorBrush) new BrushConverter().ConvertFrom("#F57C00");
        private string _StatusBarContent;
        private string _StatusBarDownloads;
        private string _StatusBarClock;

        private Visibility _loadingReadyVisibility;
        private float _dialogOpacity;
        private float _loaderOpacity;

        private string _NickName;

        private string _ActivePage;

        public Visibility LoadingReadyVisibility
        {
            get { return _loadingReadyVisibility; }
            set
            {
                _loadingReadyVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LoadingReadyVisibility"));
            }
        }

        public float DialogOpacity
        {
            get { return _dialogOpacity; }
            set
            {
                _dialogOpacity = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DialogOpacity"));
            }
        }

        public float LoaderOpacity
        {
            get { return _loaderOpacity; }
            set
            {
                _loaderOpacity = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LoaderOpacity"));
            }
        }

        public SolidColorBrush StatusBarBrush
        {
            get { return _StatusBarBrush; }
            set
            {
                _StatusBarBrush = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StatusBarBrush"));
            }
        }

        public string NickName
        {
            get { return _NickName; }
            set
            {
                _NickName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NickName"));
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
            var brush = Application.Current.Resources["AccentColorBrush"] as SolidColorBrush;
            StatusBarBrush = brush;
            SetGamesColor(brush);
            SetMusicColor(brush);
        }

        private void MetroWindow_LostFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            var brush = new BrushConverter().ConvertFrom("#808080") as SolidColorBrush;
            StatusBarBrush = brush;
            SetGamesColor(brush);
            SetMusicColor(brush);
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
    }
}