using System.Collections.Generic;
using System.Data.Entity;

using Lyudmila.API.Models;

namespace Lyudmila.API
{
    public class LyudmilaAPIContext
    {
        private static LyudmilaAPIContext _instance;

        private LyudmilaAPIContext()
        {
            Players = new List<Player>();
        }

        public static LyudmilaAPIContext GetInstance() => _instance ?? (_instance = new LyudmilaAPIContext());

        public List<Player> Players { get; set; }
    }
}
