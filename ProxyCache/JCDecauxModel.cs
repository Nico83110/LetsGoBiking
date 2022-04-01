using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;



namespace ProxyCache
{
    [DataContract]
    public class JCDecauxModel
    {
        private static readonly string API_key = "c8e8deaba5b4c9af98626d23aad7bff03fe3b1b9";
        private static readonly string url = "https://api.jcdecaux.com/vls/v2/";
        private static readonly string data = "stations";

        private string request;
        private static readonly HttpClient client = new HttpClient();

        [DataMember]
        public StationModel station { get; set; }

        public JCDecauxModel()
        {

        }


    }

    [DataContract]
    public class StationModel
    {
        
    }

    [DataContract]
    public class PositionModel
    {
       
    }
}
