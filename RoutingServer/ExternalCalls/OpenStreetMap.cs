using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using ProxyCache;

namespace RoutingServer.ExternalCalls
{
    internal class OpenStreetMap
    {
        private static HttpClient client = new HttpClient();

        public OpenStreetMap()
        {
        }
        public Position GetPositionOfAddress(string address)
        {
            return null;
        }

    }

    public class Place
    {
        public float importance { get; set; }
        public string lat { get; set; }

        public string lon { get; set; }

    }
}
