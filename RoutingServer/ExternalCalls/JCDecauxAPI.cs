using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using ProxyCache;
using System.Text.Json;
using System.ServiceModel;

namespace RoutingServer.ExternalCalls
{
    public class JCDecauxAPI
    {
        private static HttpClient client = new HttpClient();
        private static string apiKey = "c8e8deaba5b4c9af98626d23aad7bff03fe3b1b9";
        private static string stationsRequest = "https://api.jcdecaux.com/vls/v2/stations?apiKey=" + apiKey;

        public JCDecauxAPI()
        {

        }

        //Get the list of all the existing stations
        public async Task<String> GetStations()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(stationsRequest);
                response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(body);
                return body;
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException();
            }
        }
    }
}
