// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System.IO;

namespace Lyudmila.Server.Models
{
    internal class HttpBuilder
    {
        public static HttpResponse InternalServerError()
            => new HttpResponse {ReasonPhrase = "InternalServerError", StatusCode = "500", ContentAsUTF8 = File.ReadAllText("Resources/Pages/500.html")};

        public static HttpResponse NotFound()
            => new HttpResponse {ReasonPhrase = "NotFound", StatusCode = "404", ContentAsUTF8 = File.ReadAllText("Resources/Pages/404.html")};
    }
}