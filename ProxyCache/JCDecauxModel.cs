using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Threading.Tasks;


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
        public StationModelStatic station { get; set; }

        public JCDecauxModel()
        {

        }

        public JCDecauxModel(Dictionary<string, string> infos)
        {
            request = url + path + "/" + infos["station_number"] + "?contract=" + infos["contract_name"] + "&apiKey=" + API_key;
            station = CallREST(request).Result;
        }

        private static async Task<StationModelStatic> CallREST(string request)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(request);
                response.EnsureSuccessStatusCode();
                string returnedBody = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<StationModelStatic>(returnedBody);
            }
            catch (HttpRequestException)
            {
                return new StationModelStatic();
            }
        }


    }

    //Data defined as in the JCDecaux API

    [DataContract]
    public class StationModelStatic
    {
        /** Static data **/

        [DataMember]
        public int number { get; set; } //Unique number inside the station

        [DataMember]
        public string contractName { get; set; } //The contract name of this station

        [DataMember]
        public string name { get; set; } //The name of this station

        [DataMember]
        public string address  { get; set; } //An indicative address of the station, it's more like a comment

        [DataMember]
        public Position position { get; set; } //The coordinates in the WGS84 format

        [DataMember]
        public bool banking { get; set; } //Indicates if there is a payment terminal

        [DataMember]
        public bool bonus { get; set; } //Indicates if it's a "bonus" station

        [DataMember]
        public bool overflow { get; set; } //Indicates if the station accepts the bike rack in "overflow"

        [DataMember]
        public object shape { get; set; } //Not used at the moment...

    }

    [DataContract]
    public class Position
    {
        [DataMember]
        public double latitude { get; set; }

        [DataMember]
        public double longitude { get; set; }
    }


}
