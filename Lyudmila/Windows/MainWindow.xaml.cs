// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

using Lyudmila.Properties;

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

        private void GetServerIP()
        {
            Notify("Searching for server...");

            var udpclient = new UdpClient();
            var client = new UdpClient();

            var multicastaddress = IPAddress.Parse("239.0.0.222");

            var remoteEp = new IPEndPoint(multicastaddress, 2222);
            var localEp = new IPEndPoint(IPAddress.Any, 2222);

            client.Client.Bind(localEp);
            client.JoinMulticastGroup(multicastaddress);
            udpclient.JoinMulticastGroup(multicastaddress);

            var buffer = Encoding.Unicode.GetBytes("$SENDIP$");
            udpclient.Send(buffer, buffer.Length, remoteEp);

            while(true)
            {
                var data = client.Receive(ref localEp);
                var strData = Encoding.Unicode.GetString(data);

                if(!strData.Equals("$SENDIP$"))
                {
                    Settings.Default.ServerIP = strData;
                    Notify($"Found server at {strData}!");
                    break;
                }
            }
        }

        private async void Notify(string message)
        {
            Notifier.Display(message);
            Dispatcher.Invoke(() => { NotificationFlyout.IsOpen = true; });

            await Task.Delay(2000);

            Dispatcher.Invoke(() =>
            {
                if(NotificationFlyout.IsOpen)
                {
                    NotificationFlyout.IsOpen = false;
                }
            });
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

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(Settings.Default.Username))
            {
                new Thread(WaitForNickname).Start();
            }
            else
            {
                new Thread(GetServerIP).Start();
            }
        }

        private void WaitForNickname()
        {
            Dispatcher.Invoke(() =>
            {
                NicknameFlyout.IsOpen = true;
                NicknameFlyout.ClosingFinished += NickCheck;
            });
        }

        private void NickCheck(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(Settings.Default.Username))
            {
                NicknameFlyout.IsOpen = true;
            }
            else
            {
                new Thread(GetServerIP).Start();
            }
        }
    }
}