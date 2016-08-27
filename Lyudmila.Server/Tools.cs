// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.IO;
using System.Reflection;

namespace Lyudmila.Server
{
    internal class Tools
    {
        public static void ColoredWrite(ConsoleColor color, string text)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text + Environment.NewLine);
            Console.ForegroundColor = originalColor;
        }

        public static void SemiColoredWrite(ConsoleColor color, string coloredText, string noColorText)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(coloredText);
            Console.ForegroundColor = originalColor;
            Console.Write(noColorText + Environment.NewLine);
        }

        public static string Time() => $"[{DateTime.Now.ToString("dd-MM-yyyy @ HH:mm")}] ";

        public static void CreateFile(string file)
        {
            if(file == "games.json")
            {
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Lyudmila.Server.Resources.games.json"))
                {
                    using (var fileStream = new FileStream(Path.Combine(Environment.CurrentDirectory, file), FileMode.Create))
                    {
                        for (var i = 0; i < stream.Length; i++)
                        {
                            fileStream.WriteByte((byte)stream.ReadByte());
                        }
                        fileStream.Close();
                    }
                }
            }
        }

        public static void ForceClose(Exception ex)
        {
            Console.WriteLine($"{ex.GetType()}: {ex.Message}");
            Console.ReadKey(true);
            Environment.Exit(1);
        }

        public static void ForceClose(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey(true);
            Environment.Exit(1);
        }
    }
}