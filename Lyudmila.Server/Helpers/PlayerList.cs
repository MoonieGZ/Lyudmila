// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System.IO;

namespace Lyudmila.Server.Helpers
{
    internal class PlayerList
    {
        private static string PlayersToAdd;

        public static void PrepareAddToPlayersJS(string name, string ip)
        {
            PlayersToAdd = PlayersToAdd + $"<tr><td style=\"width: 50%;\"><span style=\"color: #0099cc\">{name}</span></td><td style=\"width: 15%\"><span style=\"color: #0099cc\">{ip}</span></td>";
        }

        public static void AddToPlayersJS() //TODO: Fix this
        {
            File.WriteAllText(@"C:\Users\William\Documents\Visual Studio 2015\Projects\Lyudmila\Lyudmila.Server\bin\Debug\Web\bundles\Players.js", string.Empty);
            var FinalJS = "var data='" + PlayersToAdd + "';";
            File.WriteAllText(@"C:\Users\William\Documents\Visual Studio 2015\Projects\Lyudmila\Lyudmila.Server\bin\Debug\Web\bundles\Players.js", FinalJS);
        }
    }
}