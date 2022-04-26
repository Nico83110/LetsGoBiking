using ProxyCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
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

        public JCDecauxItem GetStationInfos(string contract_name, string stationNumber)
        {
            /** REST CALL 
            //TODO : Update the ProxyCache config to manage this request
            string request = "http://localhost:8733/ProxyCache/api/station?city=" + contract_name + "&number=" + stationNumber;
            Console.WriteLine("GetStationInfos() generated request is : " + request);
            return await GetStationInfos(request);
            **/

            /** SOAP call **/
            BasicHttpBinding binding = new BasicHttpBinding
            {
                MaxBufferSize = 2000000,
                MaxReceivedMessageSize = 2000000
            };
            EndpointAddress endpoint = new EndpointAddress("http://localhost:8733/ProxyCache/Service1/");
            ChannelFactory<IJCDecauxService> myChannelFactory = new ChannelFactory<IJCDecauxService>(binding, endpoint);
            IJCDecauxService wcfClient = myChannelFactory.CreateChannel();
            Console.WriteLine("SOAP call to the Proxy...");
            JCDecauxItem result = wcfClient.GetStationDefault(contract_name, stationNumber);

            return result;
        }

        public async Task<JCDecauxItem> GetStationInfos(string request)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(request);

               // response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();
                Console.WriteLine("PROXY CALL : " + body);
                return JsonSerializer.Deserialize<JCDecauxItem>(body);
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException();
            }

        }


    }
}
