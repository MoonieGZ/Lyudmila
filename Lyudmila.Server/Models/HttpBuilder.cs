// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System.IO;

namespace Lyudmila.Server.Models
{
    internal class HttpBuilder
    {
        public static HttpResponse InternalServerError()
        {
            var content = File.ReadAllText("Resources/Pages/500.html");

            return new HttpResponse {ReasonPhrase = "InternalServerError", StatusCode = "500", ContentAsUTF8 = content};
        }

        public static HttpResponse NotFound()
        {
            var content = File.ReadAllText("Resources/Pages/404.html");

            return new HttpResponse {ReasonPhrase = "NotFound", StatusCode = "404", ContentAsUTF8 = content};
        }
    }
}