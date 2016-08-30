// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.IO;

namespace Lyudmila.Server
{
    public class Logger
    {
        private static string _currentFile = string.Empty;
        private static readonly string _logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");

        public static void Init()
        {
            if(!Directory.Exists(_logDirectory))
            {
                try
                {
                    Directory.CreateDirectory(_logDirectory);
                }
                catch(Exception ex)
                {
                    Tools.ForceClose(ex);
                }
            }
            _currentFile = DateTime.Now.ToString("yyyy-MM-dd - HH.mm.ss");
            Log($"Log started ({DateTime.Now})");
        }

        private static void Log(string message)
        {
            using(var log = File.AppendText(Path.Combine(_logDirectory, _currentFile + ".txt")))
            {
                log.WriteLine(message);
                log.Flush();
            }
        }

        public static void Write(string message, LogLevel level = LogLevel.None)
        {
            var dateFormat = Tools.Time();
            switch(level)
            {
                case LogLevel.None:
                    message = $"[NONE]    {message}";
                    Tools.ColoredWrite(ConsoleColor.White, message);
                    Log($"[{dateFormat}] {message}");
                    break;
                case LogLevel.Info:
                    message = $"[INFO]    {message}";
                    Tools.ColoredWrite(ConsoleColor.Green, message);
                    Log($"[{dateFormat}] {message}");
                    break;
                case LogLevel.Warning:
                    message = $"[WARN]    {message}";
                    Tools.ColoredWrite(ConsoleColor.Yellow, message);
                    Log($"[{dateFormat}] {message}");
                    break;
                case LogLevel.Error:
                    message = $"[ERROR]   {message}";
                    Tools.ColoredWrite(ConsoleColor.Red, message);
                    Log($"[{dateFormat}] {message}");
                    break;
                case LogLevel.Console:
                    message = $"[CONSOLE] {message}";
                    Tools.ColoredWrite(ConsoleColor.Magenta, message);
                    Log($"[{dateFormat}] {message}");
                    break;
                case LogLevel.HTTP:
                    message = $"[HTTP]    {message}";
                    Tools.ColoredWrite(ConsoleColor.DarkGray, message);
                    Log($"[{dateFormat}] {message}");
                    break;
                case LogLevel.UDP:
                    message = $"[UDP]     {message}";
                    Tools.ColoredWrite(ConsoleColor.DarkMagenta, message);
                    Log($"[{dateFormat}] {message}");
                    break;
                case LogLevel.Debug:
                    message = $"[DEBUG]   {message}";
                    Tools.ColoredWrite(ConsoleColor.Cyan, message);
                    Log($"[{dateFormat}] {message}");
                    break;
            }
        }
    }

    public enum LogLevel
    {
        None,
        Info,
        Warning,
        Error,
        Debug,
        Console,
        HTTP,
        UDP
    }
}