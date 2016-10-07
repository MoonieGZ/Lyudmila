// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using Lyudmila.Server.Helpers;
using Lyudmila.Server.Models;
using Lyudmila.Server.RouteHandlers;

namespace Lyudmila.Server
{
    internal class Program
    {
        public static readonly int httpPort = 8081;
        public static HttpServer httpServer = new HttpServer(httpPort, Routes.GET);

        private const int port = 54545;
        private const string broadcastAddress = "192.168.0.255";
        private static string MyIP = string.Empty;
        public static UdpClient receivingClient;
        public static UdpClient sendingClient;

        public static List<string> tempBuffer = new List<string>();

        private static void Main(string[] args)
        {
            Console.Title = "Lyudmila Server App";
            Logger.Init();

            if (!Directory.Exists("Web"))
            {
                Directory.CreateDirectory("Web");
            }

            Logger.Write("Starting Web Server...", LogLevel.HTTP);
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

            Commands.Quit();

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

        public static void SendToClients(string message)
        {
            var data = Encoding.ASCII.GetBytes(message);
            sendingClient.Send(data, data.Length);

            Logger.Write($"> {message}", LogLevel.Debug);
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
                        Logger.Write($"< {message} ({endPoint.Address})", LogLevel.Debug);

                        Handle(message, endPoint.Address);
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

        private static void Handle(string message, IPAddress address)
        {
            if(message.Equals("$SERVERREQUEST$"))
            {
                SendToClients("$SERVER$");
            }
            if(message.Contains("$CLIENT$"))
            {
                tempBuffer.Add($"{message.Split(new[] {"$CLIENT$ "}, StringSplitOptions.None)[1]} @ {address}");
            }
            if(message.StartsWith("$ADMINLOGIN$ "))
            {
                //Administration.Login(message.Split(new[] {"$ADMINLOGIN$ "}, StringSplitOptions.None)[1], address);
            }
        }
    }
}