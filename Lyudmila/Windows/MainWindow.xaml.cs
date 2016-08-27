// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.Windows;

namespace Lyudmila.Windows
{
    /// <summary>
    ///   Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowSettings(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ShowFriends(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ShowMusic(object sender, RoutedEventArgs e)
        {
            MusicFlyout.IsOpen = !MusicFlyout.IsOpen;
        }
    }
}