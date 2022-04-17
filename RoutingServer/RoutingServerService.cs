using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ProxyCache;
using RoutingServer.ExternalCalls;

namespace RoutingServer
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class RoutingServerService : IRoutingServerService
    {
        private static JCDecauxAPI jCDecauxAPI = new JCDecauxAPI();
        private static ProxyCall proxy = new ProxyCall();

        public List<StationModel> GetAllStations()
        {
            return jCDecauxAPI.GetStations().Result;
        }

        public StationModel GetSpecificStation(string city, string station_number)
        {
            StationModel station = proxy.GetStationInfos(city, station_number).Result;
            return station;
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
