// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Lyudmila.Server
{
    internal class Commands
    {
        public static void Help(string param, Dictionary<string, CommandRecord> commandMap)
        {
            var helpText = "Available commands: \r\nquit: Exit the program";
            foreach(var s in commandMap.Keys)
            {
                helpText += "\r\n" + s + ": ";
                helpText += commandMap[s].helpText;
            }
            Logger.Write(helpText, LogLevel.Console);
        }

        public static void Clear(string param, Dictionary<string, CommandRecord> commandmap) => Console.Clear();
    }
}