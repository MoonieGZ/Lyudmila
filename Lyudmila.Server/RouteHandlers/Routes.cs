// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;

using Lyudmila.Server.Models;

namespace Lyudmila.Server.RouteHandlers
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
                        Callable = new FileSystemRouteHandler {BasePath = Path.Combine(Environment.CurrentDirectory, "Web")}.Handle,
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