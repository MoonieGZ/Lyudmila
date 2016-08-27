// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.IO;
using System.Threading;

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
        }
    }
}