using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace RoutingServer.ExternalCalls
{
    internal class JCDecauxAPI
    {
        private static HttpClient client = new HttpClient();
        private static string apiKey = "c8e8deaba5b4c9af98626d23aad7bff03fe3b1b9"
        private static string stationsRequest = "https://api.jcdecaux.com/vls/v2/stations?apiKey=" + apiKey;

        public JCDecauxAPI()
        {

        }
    }
}
