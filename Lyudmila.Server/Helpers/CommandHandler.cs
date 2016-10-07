// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System.Collections.Generic;

namespace Lyudmila.Server.Helpers
{
    public struct CommandRecord
    {
        public string commandName, helpText;
        public CommandHost.CommandHandler handler;
    }

    public class CommandHost
    {
        public delegate void CommandHandler(string param, Dictionary<string, CommandRecord> commandMap);

        public readonly Dictionary<string, CommandRecord> commandMap = new Dictionary<string, CommandRecord>();

        public CommandHost()
        {
            RegisterCommand("help", "Show available commands", Commands.Help);
            RegisterCommand("clear", "Clear the console display", Commands.Clear);
            RegisterCommand("clients", "Show all connected clients", Commands.Clients);
            RegisterCommand("flush", "Clear all log files", Commands.Flush);
            RegisterCommand("games", "Manage available games", Commands.Games);
            RegisterCommand("ts_clients", "View connected clients on TeamSpeak", Commands.TeamSpeak);
        }

        public void RegisterCommand(string commandName, string helpText, CommandHandler handler)
        {
            var record = new CommandRecord {commandName = commandName, helpText = helpText, handler = handler};
            commandMap.Add(commandName, record);
        }

        public bool InvokeCommand(string inputLine)
        {
            inputLine = inputLine.TrimStart();

            var commandNameLength = 0;
            
            for (; commandNameLength < inputLine.Length && inputLine[commandNameLength] != ' '; commandNameLength++) {}

            var commandName = inputLine.Substring(0, commandNameLength);
            var param = inputLine.Substring(commandNameLength).TrimStart();

            if(commandName == "quit")
            {
                return false;
            }

            if(commandMap.ContainsKey(commandName))
            {
                commandMap[commandName].handler.Invoke(param, commandMap);
                return true;
            }

            Logger.Write($"Unknown command {commandName}", LogLevel.Console);
            return true;
        }
    }
}