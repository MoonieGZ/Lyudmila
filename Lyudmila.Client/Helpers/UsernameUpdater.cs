// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System.IO;

using Lyudmila.Client.Properties;

namespace Lyudmila.Client.Helpers
{
    internal class UsernameUpdater
    {
        public static void SetName(string gameName) // TODO
        {
            switch(gameName)
            {
                case "AoE2HD":
                    var path = Path.Combine(Settings.Default.AoE2HD_Location, "SmartSteamEmu.ini");
                    var SettingsFile = File.ReadAllLines(path);
                    var i = 0;
                    foreach (var line in SettingsFile)
                    {
                        if(line.StartsWith("PersonaName = "))
                        {
                            var newLine = line.Replace("Unreal", Settings.Default.Username);
                            SettingsFile[i] = newLine;
                            File.WriteAllLines(path, SettingsFile);
                            return;
                        }
                        i++;
                    }
                    break;
            }
        }
    }
}