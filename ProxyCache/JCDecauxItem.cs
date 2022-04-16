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
        public string contract_name { get; set; }
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
        public DateTime last_update { get; set; }
        [DataMember]
        public bool connected { get; set; }
        [DataMember]
        public bool overflow { get; set; }
        [DataMember]
        public object shape { get; set; }
        [DataMember]
        public Stands total_stands { get; set; }
        [DataMember]
        public Stands main_stands { get; set; }
        [DataMember]
        public object overflow_stands { get; set; }

        //This is used for checking the data received in the client
        public override string ToString()
        {
            return "Contract name: " + contract_name + "\nname: " + name + "\nPosition : " + position;
        }

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
        public string contract_name { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int number { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public Position position { get; set; }
        [DataMember]
        public string access_type { get; set; }
        [DataMember]
        public string locker_type { get; set; }
        [DataMember]
        public bool has_surveillance { get; set; }
        [DataMember]
        public bool is_free { get; set; }
        [DataMember]
        public string address { get; set; }
        [DataMember]
        public string zip_code { get; set; }
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public bool is_off_street { get; set; }
        [DataMember]
        public bool has_electric_support { get; set; }
        [DataMember]
        public bool has_physical_reception { get; set; }
    }


    public class Position
    {
        [DataMember]
        public float latitude { get; set; }
        [DataMember]
        public float longitude { get; set; }
        public override string ToString()
        {
            return "Latitude: " + latitude + " Longitude : " + longitude;
        }
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
        public int mechanical_bikes { get; set; }
        [DataMember]
        public int electrical_bikes { get; set; }
        [DataMember]
        public int electrical_internal_battery_bikes { get; set; }
        [DataMember]
        public int electrical_removable_battery_bikes { get; set; }
    }



}
