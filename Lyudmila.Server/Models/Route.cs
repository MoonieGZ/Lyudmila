// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;

namespace Lyudmila.Server.Models
{
    public class Route
    {
        public string Name { get; set; } // descriptive name for debugging
        public string UrlRegex { get; set; }
        public string Method { get; set; }
        public Func<HttpRequest, HttpResponse> Callable { get; set; }
    }
}