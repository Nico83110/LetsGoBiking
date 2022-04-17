using ProxyCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RoutingServer.ExternalCalls
{
    internal class ProxyCall
    {
        private static HttpClient client = new HttpClient();

        public ProxyCall()
        {
        }

        public async Task<StationModel> GetStationInfos(string contract_name, string stationNumber)
        {
            //TODO : Update the ProxyCache config to manage this request
            string request = "http://localhost:8733/ProxyCache/Service1/station?city=" + contract_name + "&number=" + stationNumber;
            return await GetStationInfos(request);
        }

        public async Task<StationModel> GetStationInfos(string request)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(request);
                response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<StationModel>(body);
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException();
            }

        }


    }
}
