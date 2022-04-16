using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer.ExternalCalls
{
    internal class ProxyCall
    {
        private static HttpClient client = new HttpClient();

        public ProxyCall()
        {
        }
    }
}
