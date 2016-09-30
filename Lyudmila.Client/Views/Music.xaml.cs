// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

using Caliburn.Micro;

using Lyudmila.Client.Helpers;
using Lyudmila.Client.Properties;
using Lyudmila.Client.Windows;

using MahApps.Metro.Controls.Dialogs;

using MaterialDesignThemes.Wpf;

using Application = System.Windows.Application;

namespace Lyudmila.Client.Views
{
    /// <summary>
    ///   Interaction logic for Music.xaml
    /// </summary>
    public partial class Music
    {
        public static bool _ready;

        private readonly MediaElement MediaPlayer = new MediaElement
        {
            Volume = 1,
            Visibility = Visibility.Collapsed,
            LoadedBehavior = MediaState.Manual,
            UnloadedBehavior = MediaState.Manual
        };

        public Music()
        {
            InitializeComponent();

            var soundEngine = NAudioEngine.Instance;

            spectrumAnalyzer.RegisterSoundPlayer(soundEngine);
            waveformTimeline.RegisterSoundPlayer(soundEngine);

            LoadSongList += LoadSongs;
        }

        public static event Action<string> LoadSongList;

        public static void Init()
        {
            if(!_ready)
            {
                if(string.IsNullOrEmpty(Settings.Default.MusicDir))
                {
                    var answer = ShowSelectFolderDialog("Select music folder:", "C:\\");
                    if(string.IsNullOrEmpty(answer))
                    {
                        ShowMessage("Lyudmila", "Music will not be available.");
                    }
                    else
                    {
                        Settings.Default.MusicDir = answer;
                        Settings.Default.Save();
                        _ready = true;
                    }
                }

                LoadSongList(null);
            }
        }

        public static string ShowSelectFolderDialog(string caption, string oldPath = null)
        {
            string folder = null;
            Execute.OnUIThread(() =>
            {
                var folderBrowser = new FolderBrowserDialog {SelectedPath = oldPath, Description = caption, ShowNewFolderButton = true};
                folder = folderBrowser.ShowDialog() == DialogResult.OK ? folderBrowser.SelectedPath : null;
            });
            return folder;
        }

        private static async void ShowMessage(string title, string message)
        {
            var m = new MetroDialogSettings
            {
                AffirmativeButtonText = "OK",
                AnimateShow = true,
                AnimateHide = true,
                SuppressDefaultResources = true,
                CustomResourceDictionary = new ResourceDictionary {Source = new Uri("pack://application:,,,/Resources/Themes/Dialogs.xaml")}
            };
            await ((MainWindow) Application.Current.MainWindow).ShowMessageAsync(title, message, MessageDialogStyle.Affirmative, m);
        }

        private void Btn_PlayPause(object sender, RoutedEventArgs e)
        {
            if(NAudioEngine.Instance.IsPlaying)
            {
                if(NAudioEngine.Instance.CanPause)
                {
                    NAudioEngine.Instance.Pause();
                    PlayPauseIcon.Kind = PackIconKind.Play;
                }
            }
            else
            {
                if(NAudioEngine.Instance.CanPlay)
                {
                    NAudioEngine.Instance.Play();
                    PlayPauseIcon.Kind = PackIconKind.Pause;
                }
            }
        }

        private void LoadSongs(string n = null)
        {
            SongList.Items.Clear();

            var files = Directory.GetFiles(Settings.Default.MusicDir, "*.mp3");

            foreach(var toAdd in files.Select(file => file.Split('\\').Last()).Select(toAdd => toAdd.Substring(0, toAdd.Length - 4)))
            {
                SongList.Items.Add(toAdd);
            }
        }

        private void VolumeChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {}

        private void SongList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MediaPlayer.Source = new Uri(Path.Combine(Settings.Default.MusicDir, SongList.SelectedItem + ".mp3"));
            SongTitle.Content = SongList.SelectedItem.ToString();

            NAudioEngine.Instance.OpenFile(Path.Combine(Settings.Default.MusicDir, SongList.SelectedItem + ".mp3"));
            NAudioEngine.Instance.Play();

            PlayPauseIcon.Kind = PackIconKind.Pause;
        }

        private void ChangeFolder(object sender, RoutedEventArgs e)
        {
            if(NAudioEngine.Instance.CanPause)
                NAudioEngine.Instance.Pause();
            var answer = ShowSelectFolderDialog("Select music folder:", "C:\\");
            if(!string.IsNullOrEmpty(answer))
            {
                Settings.Default.MusicDir = answer;
                Settings.Default.Save();

                LoadSongList(null);
            }
        }

        private void Btn_Previous(object sender, RoutedEventArgs e)
        {
            foreach(var song in SongList.Items)
            {
                var currentSong = NAudioEngine.Instance.FileTag.Name.Split('\\').Last().Replace(".mp3", "");

                if(song.Equals(currentSong))
                {
                    var index = SongList.Items.IndexOf(currentSong) - 1;
                    SongList.SelectedIndex = index == -1 ? SongList.Items.Count - 1 : index;

                    PlayNew();

                    break;
                }
            }
        }

        private void Btn_Next(object sender, RoutedEventArgs e)
        {
            foreach(var song in SongList.Items)
            {
                var currentSong = NAudioEngine.Instance.FileTag.Name.Split('\\').Last().Replace(".mp3", "");

                if(song.Equals(currentSong))
                {
                    var index = SongList.Items.IndexOf(currentSong) + 1;
                    SongList.SelectedIndex = index > SongList.Items.Count ? 0 : index;

                    PlayNew();

                    break;
                }
            }
        }

        private void PlayNew()
        {
            NAudioEngine.Instance.OpenFile(Path.Combine(Settings.Default.MusicDir, SongList.SelectedItem + ".mp3"));
            NAudioEngine.Instance.Play();

            SongTitle.Content = SongList.SelectedItem.ToString();
        }
    }
}