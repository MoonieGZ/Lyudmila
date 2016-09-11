// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using Lyudmila.Server.Models;

namespace Lyudmila.Server.Helpers
{
    public class HttpServer
    {
        private readonly int Port;
        private TcpListener Listener;
        private readonly HttpProcessor Processor;
        public bool IsActive;

        public HttpServer(int port, IEnumerable<Route> routes)
        {
            Port = port;
            Processor = new HttpProcessor();

            foreach(var route in routes)
            {
                Processor.AddRoute(route);
            }
        }

        public void Listen()
        {
            Listener = new TcpListener(IPAddress.Any, Port);
            Listener.Start();
            IsActive = true;

            while(IsActive)
            {
                var s = Listener.AcceptTcpClient();
                var thread = new Thread(() => { Processor.HandleClient(s); });
                thread.Start();
                Thread.Sleep(1);
            }
        }

        public static void IPList()
        {
            foreach(var ipaddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ipaddress => ipaddress.ToString().StartsWith("192."))) {
                Logger.Write($"Listening on {ipaddress}:{Program.httpPort}", LogLevel.HTTP);
            }
        }
    }
}