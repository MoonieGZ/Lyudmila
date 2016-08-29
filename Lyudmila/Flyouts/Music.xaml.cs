// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

using MaterialDesignThemes.Wpf;

namespace Lyudmila.Flyouts
{
    /// <summary>
    ///   Interaction logic for Music.xaml
    /// </summary>
    public partial class Music
    {
        private readonly DispatcherTimer playTimer = new DispatcherTimer();
        private readonly MediaElement MediaPlayer = new MediaElement { Volume = 1, Visibility = Visibility.Collapsed, LoadedBehavior = MediaState.Manual, UnloadedBehavior = MediaState.Manual };
        private bool isPlaying;

        public Music()
        {
            InitializeComponent();

            MediaPlayer.MediaOpened += Element_MediaOpened;
            MediaPlayer.MediaEnded += Element_MediaEnded;

            playTimer.Interval = TimeSpan.FromMilliseconds(200);
            playTimer.Tick += playTimer_Tick;
        }

        private void Btn_PlayPause(object sender, RoutedEventArgs e)
        {
            if(isPlaying)
            {
                MediaPlayer.Pause();
                PlayPauseIcon.Kind = PackIconKind.Play;
                isPlaying = false;
            }
            else
            {
                MediaPlayer.Play();
                PlayPauseIcon.Kind = PackIconKind.Pause;
                isPlaying = true;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SongList.Items.Clear();

            var files = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "music"), "*.mp3");

            foreach(var toAdd in files.Select(file => file.Split('\\').Last()).Select(toAdd => toAdd.Substring(0, toAdd.Length - 4)))
            {
                SongList.Items.Add(toAdd);
            }
        }

        private void VolumeChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MediaPlayer.Volume = e.NewValue;
        }

        private void SongList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MediaPlayer.Source = new Uri(Path.Combine(Environment.CurrentDirectory, "music", SongList.SelectedItem + ".mp3"));
            SongTitle.Content = SongList.SelectedItem.ToString();
            MediaPlayer.Play();
            isPlaying = true;
            PlayPauseIcon.Kind = PackIconKind.Pause;
        }

        private void playTimer_Tick(object sender, EventArgs e)
        {
            var current = MediaPlayer.Position.TotalSeconds;
            var max = MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;

            timelineSlider.Value = current / max * 100;
            progressLabel.Content = $"{MediaPlayer.Position.TotalMinutes:0}:{MediaPlayer.Position.TotalSeconds:00} / "
                                    + $"{new DateTime(MediaPlayer.NaturalDuration.TimeSpan.Ticks).ToString("mm:ss")}";
        }

        private async void Element_MediaOpened(object sender, RoutedEventArgs e)
        {
            await Task.Delay(10);
            playTimer.Start();
        }

        private void Element_MediaEnded(object sender, EventArgs e)
        {
            MediaPlayer.Stop();
            playTimer.Stop();
        }

        private void timelineSlider_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isPlaying)
            {
                playTimer.Stop();
            }
        }

        private void timelineSlider_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isPlaying)
            {
                MediaPlayer.Position = new TimeSpan(0, 0, 0, (int)(timelineSlider.Value * MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds / 100));
                playTimer.Start();
            }
        }
    }
}