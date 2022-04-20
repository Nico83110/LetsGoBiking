using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer.ExternalCalls
{
    public class OpenRouteServiceAPI
    {
        private HttpClient client = new HttpClient();
        private string apiKey = "5b3ce3597851110001cf6248386a163bffd64098bce61b52dfc5ed87";

        public OpenRouteServiceAPI()
        {
        }
    }

}
