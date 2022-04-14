using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System;


namespace ProxyCache
{
    [DataContract]
    public class JCDecauxItem
    {
        private static readonly string API_key = "c8e8deaba5b4c9af98626d23aad7bff03fe3b1b9";
        private static readonly string url = "https://api.jcdecaux.com/vls/v2/";
        private static readonly string path = "stations";

        private string request;
        private static readonly HttpClient client = new HttpClient();

        [DataMember]
        public StationModel station { get; set; }

        public JCDecauxItem()
        {

        }

        // JCDecauxItem class with a constructor which makes a request to the JCDecaux API to create a JCDecauxItem object.
        public JCDecauxItem(Dictionary<string, string> infos)
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
        [DataMember]
        public int number { get; set; }
        [DataMember]
        public string contractName { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string address { get; set; }
        [DataMember]
        public Position position { get; set; }
        [DataMember]
        public bool banking { get; set; }
        [DataMember]
        public bool bonus { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public DateTime lastUpdate { get; set; }
        [DataMember]
        public bool connected { get; set; }
        [DataMember]
        public bool overflow { get; set; }
        [DataMember]
        public object shape { get; set; }
        [DataMember]
        public Stands totalStands { get; set; }
        [DataMember]
        public Stands mainStands { get; set; }
        [DataMember]
        public object overflowStands { get; set; }

    }


    [DataContract]
    public class ContractModel
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string commercial_name { get; set; }
        [DataMember]
        public string country_code { get; set; }
        [DataMember]
        public string[] cities { get; set; }
    }


    [DataContract]
    public class ParkingModel
    {
        [DataMember]
        public string contractName { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int number { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public Position position { get; set; }
        [DataMember]
        public string accessType { get; set; }
        [DataMember]
        public string lockerType { get; set; }
        [DataMember]
        public bool hasSurveillance { get; set; }
        [DataMember]
        public bool isFree { get; set; }
        [DataMember]
        public string address { get; set; }
        [DataMember]
        public string zipCode { get; set; }
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public bool isOffStreet { get; set; }
        [DataMember]
        public bool hasElectricSupport { get; set; }
        [DataMember]
        public bool hasPhysicalReception { get; set; }
    }


    public class Position
    {
        [DataMember]
        public float latitude { get; set; }
        [DataMember]
        public float longitude { get; set; }
    }

    public class Stands
    {
        [DataMember]
        public Availabilities availabilities { get; set; }
        [DataMember]
        public int capacity { get; set; }
    }

    public class Availabilities
    {
        [DataMember]
        public int bikes { get; set; }
        [DataMember]
        public int stands { get; set; }
        [DataMember]
        public int mechanicalBikes { get; set; }
        [DataMember]
        public int electricalBikes { get; set; }
        [DataMember]
        public int electricalInternalBatteryBikes { get; set; }
        [DataMember]
        public int electricalRemovableBatteryBikes { get; set; }
    }



}
