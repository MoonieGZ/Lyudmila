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

namespace Lyudmila.Server
{
    internal class Program
    {
        public static readonly int httpPort = 8080;

        private const int port = 54545;
        private const string broadcastAddress = "192.168.0.255";
        private static string MyIP = string.Empty;
        private static UdpClient receivingClient;
        private static UdpClient sendingClient;

        private static void Main(string[] args)
        {
            Console.Title = "Lyudmila Server App";
            Logger.Init();

            if (!Directory.Exists("Web"))
            {
                Directory.CreateDirectory("Web");
            }
            if (!File.Exists("Web\\games.json"))
            {
                Tools.CreateFile("Web\\games.json");
                Tools.ForceClose("Games list is not ready for deployment.");
            }

            Logger.Write("Starting Web Server...", LogLevel.Info);
            var httpServer = new HttpServer(httpPort, Routes.GET);
            new Thread(httpServer.Listen).Start();
            HttpServer.IPList();

            MyIP = LocalIPAddress();
            InitializeSender();
            InitializeReceiver();

            var cmdHost = new CommandHost();

            var running = cmdHost.InvokeCommand(Console.ReadLine());
            while(running)
            {
                Console.ForegroundColor = ConsoleColor.White;
                running = cmdHost.InvokeCommand(Console.ReadLine());
            }

            Logger.Write("Stopping Web Server...", LogLevel.HTTP);
            httpServer.IsActive = false;
            Thread.Sleep(500);
            Logger.Write("Stopping receiver...", LogLevel.UDP);
            receivingClient.Close();
            Thread.Sleep(500);
            Logger.Write("Stopping sender...", LogLevel.UDP);
            sendingClient.Close();
            Thread.Sleep(1000);
            Environment.Exit(0);
        }

        public static string LocalIPAddress()
        {
            var localIP = string.Empty;
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach(var ip in host.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork))
            {
                localIP = ip.ToString();
                break;
            }
            return localIP;
        }

        private static void InitializeReceiver()
        {
            receivingClient = new UdpClient(port);
            new Thread(Receiver).Start();

            Logger.Write($"Listening to client requests on {MyIP}", LogLevel.UDP);
        }

        private static void InitializeSender()
        {
            sendingClient = new UdpClient(broadcastAddress, port) {EnableBroadcast = true};
        }

        private static void SendToClients(string message)
        {
            // TODO: Send info to other clients.

            var data = Encoding.ASCII.GetBytes(message);
            sendingClient.Send(data, data.Length);

            Logger.Write($"> {message}", LogLevel.UDP);
        }

        private static void Receiver()
        {
            var endPoint = new IPEndPoint(IPAddress.Any, port);

            while(true)
            {
                try
                {
                    var data = receivingClient.Receive(ref endPoint);
                    var message = Encoding.ASCII.GetString(data);

                    if(!endPoint.Address.ToString().Equals(MyIP))
                    {
                        Logger.Write($"< {message} ({endPoint.Address})", LogLevel.UDP);

                        // TODO: Handle incoming messages here
                        Handle(message);
                    }
                }
                catch(Exception ex)
                {
                    if(ex.Message.Equals("A blocking operation was interrupted by a call to WSACancelBlockingCall"))
                    {
                        break; // Shutdown causes this, we break to actually stop the receiver.
                    }
                    Logger.Write($"{ex.GetType()}: {ex.Message}", LogLevel.Error);
                }
            }
        }

        private static void Handle(string message)
        {
            if(message.Equals("$SERVERREQUEST$"))
            {
                SendToClients("$SERVER$");
            }
        }
    }
}