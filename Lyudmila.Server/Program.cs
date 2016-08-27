// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lyudmila.Server
{
    internal class Program
    {
        public static int port = 8080;

        private static void Main(string[] args)
        {
            Console.Title = "Lyudmila Server App";
            Logger.Init();

            if(!File.Exists("games.json"))
            {
                Tools.CreateFile("games.json");
                Tools.ForceClose("Games list is not ready for deployment.");
            }

            Logger.Write("Starting Web Server...", LogLevel.Info);
            new Thread(new HttpServer(port, Routes.GET).Listen).Start();
            HttpServer.IPList();

            new Thread(WaitForRequests).Start();
        }

        private static async void WaitForRequests()
        {
            Logger.Write("Starting UDP broadcaster...", LogLevel.Info);

            var client = new UdpClient();
            var localEp = new IPEndPoint(IPAddress.Any, 2222);
            client.Client.Bind(localEp);
            client.JoinMulticastGroup(IPAddress.Parse("239.0.0.222"));

            while(true)
            {
                var data = client.Receive(ref localEp);
                var strData = Encoding.Unicode.GetString(data);

                if(strData.Equals("$SENDIP$"))
                {
                    await Task.Delay(200);
                    SendServerIP();
                }
            }
        }

        private static void SendServerIP()
        {
            var udpclient = new UdpClient();

            var multicastaddress = IPAddress.Parse("239.0.0.222");
            udpclient.JoinMulticastGroup(multicastaddress);
            var remoteep = new IPEndPoint(multicastaddress, 2222);

            foreach(var address in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if(address.ToString().StartsWith("192.168.0."))
                {
                    var buffer = Encoding.Unicode.GetBytes(address.ToString());
                    udpclient.Send(buffer, buffer.Length, remoteep);
                    Logger.Write($"Sent {address} to {remoteep}", LogLevel.Info);
                }
            }
        }
    }
}