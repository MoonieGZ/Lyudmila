// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Lyudmila.Server
{
    internal class Commands
    {
        public static void Help(string param, Dictionary<string, CommandRecord> commandMap)
        {
            var helpText = "Available commands: \r\nquit: Exit the program";
            foreach(var s in commandMap.Keys)
            {
                helpText += $"\r\n{s}: {commandMap[s].helpText}";
            }
            Logger.Write(helpText, LogLevel.Console);
            Logger.Write("--- help", LogLevel.Console);
        }

        public static void Clear(string param, Dictionary<string, CommandRecord> commandmap) => Console.Clear();

        public static void Flush(string param, Dictionary<string, CommandRecord> commandmap)
        {
            try
            {
                foreach (var file in new DirectoryInfo("Logs").GetFiles())
                {
                    file.Delete();
                }
            }
            catch(Exception ex)
            {
                Logger.Write($"{ex.GetType()}: {ex.Message}", LogLevel.Error);
            }
            Logger.Write("Flushed all log files.", LogLevel.Console);
        }

        public static void Quit()
        {
            Logger.Write("Stopping Web Server...", LogLevel.HTTP);
            Program.httpServer.IsActive = false;
            Thread.Sleep(500);
            Logger.Write("Stopping receiver...", LogLevel.UDP);
            Program.receivingClient.Close();
            Thread.Sleep(500);
            Logger.Write("Stopping sender...", LogLevel.UDP);
            Program.sendingClient.Close();
            Thread.Sleep(1000);
        }

        public static void Games(string param, Dictionary<string, CommandRecord> commandmap)
        {
            Logger.Write("Opening JsonBuilder for games...", LogLevel.Console);

            Application.EnableVisualStyles();
            Application.Run(new JsonBuilder());
        }
    }
}