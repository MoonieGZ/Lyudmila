// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace Lyudmila.Server.Models
{
    public class HttpRequest
    {
        public HttpRequest()
        {
            Headers = new Dictionary<string, string>();
        }

        public string Method { get; set; }
        public string Url { get; set; }
        public string Path { get; set; } // either the Url, or the first regex group
        public string Content { get; set; }
        public Route Route { get; set; }
        public Dictionary<string, string> Headers { get; set; }

        public override string ToString()
        {
            if(!string.IsNullOrWhiteSpace(Content))
            {
                if(!Headers.ContainsKey("Content-Length"))
                {
                    Headers.Add("Content-Length", Content.Length.ToString());
                }
            }

            return $"{Method} {Url} HTTP/1.0\r\n{string.Join("\r\n", Headers.Select(x => $"{x.Key}: {x.Value}"))}\r\n\r\n{Content}";
        }
    }
}