using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Lyudmila.API.Models
{
    public class Player
    {
        public int? Id { get; set; }
        public string Nickname { get; set; }
        public string Status { get; set; } // ex: ingame:cod2
        public string IpAddress { get; set; }
    }
}