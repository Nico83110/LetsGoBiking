using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System;


namespace ProxyCache
{
    [DataContract]
    public class JCDecauxModel
    {
        private static readonly string API_key = "c8e8deaba5b4c9af98626d23aad7bff03fe3b1b9";
        private static readonly string url = "https://api.jcdecaux.com/vls/v2/";
        private static readonly string path = "stations";

        private string request;
        private static readonly HttpClient client = new HttpClient();

        [DataMember]
        public StationModel station { get; set; }

        public JCDecauxModel()
        {

        }

        public JCDecauxModel(Dictionary<string, string> infos)
        {
            request = url + path + "/" + infos["station_number"] + "?contract=" + infos["contract_name"] + "&apiKey=" + API_key;
            station = CallREST(request).Result;
        }

        private static async Task<StationModel> CallREST(string request)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(request);
                response.EnsureSuccessStatusCode();
                string returnedBody = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<StationModel>(returnedBody);
            }
            catch (HttpRequestException)
            {
                return new StationModel();
            }
        }


    }


    //Data defined as in the JCDecaux API


    [DataContract]
    public class StationModel
    {
        public int number { get; set; }
        public string contractName { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public Position position { get; set; }
        public bool banking { get; set; }
        public bool bonus { get; set; }
        public string status { get; set; }
        public DateTime lastUpdate { get; set; }
        public bool connected { get; set; }
        public bool overflow { get; set; }
        public object shape { get; set; }
        public Stands totalStands { get; set; }
        public Stands mainStands { get; set; }
        public object overflowStands { get; set; }
    }


    [DataContract]

    public class ContractModel
    {
        public string name { get; set; }
        public string commercial_name { get; set; }
        public string country_code { get; set; }
        public string[] cities { get; set; }
    }

    public class Position
    {
        public float latitude { get; set; }
        public float longitude { get; set; }
    }

    public class Stands
    {
        public Availabilities availabilities { get; set; }
        public int capacity { get; set; }
    }

    public class Availabilities
    {
        public int bikes { get; set; }
        public int stands { get; set; }
        public int mechanicalBikes { get; set; }
        public int electricalBikes { get; set; }
        public int electricalInternalBatteryBikes { get; set; }
        public int electricalRemovableBatteryBikes { get; set; }
    }



}
