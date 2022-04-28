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
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class JCDecauxService : IJCDecauxService
    {
        private static string endpoint = "station";

        private static Cache<JCDecauxItem> cache = new Cache<JCDecauxItem>();

        public JCDecauxItem GetStationDefault(string city, string number)
        {
            //Default expiration time is now+60 seconds as in the subject.
            //Console.WriteLine("Entered into GetStationDefault on Proxy...");
            return GetStation(city, number, 60);
        }

        public JCDecauxItem GetStation(string city, string number, double duration)
        {
            Dictionary<string, string> infos = new Dictionary<string, string>
            {
                { "contract_name", city },
                { "station_number", number }
            };
            return cache.Get(endpoint + "?contract=" + city + "&number=" + number, duration, infos);
        }
    }

}
