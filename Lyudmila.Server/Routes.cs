// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System.Collections.Generic;

using Lyudmila.Server.Models;
using Lyudmila.Server.RouteHandlers;

namespace Lyudmila.Server
{
    internal static class Routes
    {
        public static List<Route> GET
            =>
                new List<Route>
                {
                    new Route {Callable = HomeIndex, UrlRegex = "^\\/$", Method = "GET"},
                    new Route
                    {
                        Callable = new FileSystemRouteHandler {BasePath = @"C:\Users\William\Desktop\http"}.Handle,
                        UrlRegex = "^\\/Static\\/(.*)$",
                        Method = "GET"
                    }
                };

        private static HttpResponse HomeIndex(HttpRequest request)
        {
            return new HttpResponse { ContentAsUTF8 = "🍃", ReasonPhrase = "🍂", StatusCode = "200" };
        }
    }
}