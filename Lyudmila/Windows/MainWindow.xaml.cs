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

            var multicastaddress = IPAddress.Parse("239.0.0.222");
            udpclient.JoinMulticastGroup(multicastaddress);
            var remoteep = new IPEndPoint(multicastaddress, 2222);

            var buffer = Encoding.Unicode.GetBytes("$SENDIP$");
            udpclient.Send(buffer, buffer.Length, remoteep);

            var client = new UdpClient();
            var localEp = new IPEndPoint(IPAddress.Any, 2222);
            client.Client.Bind(localEp);
            client.JoinMulticastGroup(IPAddress.Parse("239.0.0.222"));

            while(true)
            {
                var data = client.Receive(ref localEp);
                var strData = Encoding.Unicode.GetString(data);

                if (!strData.Equals("$SENDIP$"))
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
            new Thread(GetServerIP).Start();
        }
    }
}