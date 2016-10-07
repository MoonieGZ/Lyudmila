// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Lyudmila.Client.Properties;

namespace Lyudmila.Client.Helpers
{
    public class NetTools
    {
        private const int port = 54545;
        private const string broadcastAddress = "192.168.0.255";
        private static string MyIP = string.Empty;
        private static UdpClient receivingClient;
        private static UdpClient sendingClient;

        public static string LocalIPAddress()
        {
            var localIP = string.Empty;
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork))
            {
                localIP = ip.ToString();
                break;
            }
            return localIP;
        }

        public static void InitializeReceiver()
        {
            receivingClient = new UdpClient(port);
            new Thread(Receiver).Start();
        }

        private static void Receiver()
        {
            var endPoint = new IPEndPoint(IPAddress.Any, port);

            while (true)
            {
                try
                {
                    var data = receivingClient.Receive(ref endPoint);
                    var message = Encoding.ASCII.GetString(data);

                    if (message.Equals("$SERVER$"))
                    {
                        Settings.Default.ServerIP = endPoint.Address.ToString();
                        MessageBox.Show($@"Found server at {Settings.Default.ServerIP}");
                    }
                    if (message.Equals("$CLIENTLIST$"))
                    {
                        SendToClients($"$CLIENT$ {Settings.Default.Username}");
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Equals("A blocking operation was interrupted by a call to WSACancelBlockingCall"))
                    {
                        break; // Shutdown causes this, we break to actually stop the receiver.
                    }
                }
            }
        }

        private static void SendToClients(string msg)
        {
            var data = Encoding.ASCII.GetBytes(msg);
            sendingClient.Send(data, data.Length);
        }

        public static void InitializeSender()
        {
            sendingClient = new UdpClient(broadcastAddress, port) { EnableBroadcast = true };
        }

        public static void Init()
        {
            MyIP = LocalIPAddress();
            InitializeSender();
            InitializeReceiver();

            Thread.Sleep(1000);
            SendToClients("$SERVERREQUEST$");
        }
    }
}