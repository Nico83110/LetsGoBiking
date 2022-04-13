using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
//using Proxy.Cache;
//using Proxy.Models;


namespace ProxyCache
{
    public class JCDecaux : IJCDecauxService
    {
        private static string endpoint = "station";

        private static Cache<JCDecauxItem> cache = new Cache<JCDecauxItem>();

        public JCDecauxItem GetStationDefault(string city, string stationNumber)
        {
            //Default expiration time is now+60 seconds as in the subject.
            return GetStation(city, stationNumber, 60);
        }

        public JCDecauxItem GetStation(string city, string stationNumber, double duration)
        {
            Dictionary<string, string> infos = new Dictionary<string, string>
            {
                { "contract_name", city },
                { "station_number", stationNumber }
            };
            return cache.Get(endpoint + "?contract=" + city + "&number=" + stationNumber, duration/*, infos*/);
        }
    }

}
