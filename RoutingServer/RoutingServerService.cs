using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ProxyCache;
using RoutingServer.ExternalCalls;
using System.ServiceModel.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace RoutingServer
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class RoutingServerService : IRoutingServerService
    {
        private static JCDecauxAPI jCDecauxAPI = new JCDecauxAPI();
        private static ProxyCall proxy = new ProxyCall();
        private static OpenStreetMap openStreetMap = new OpenStreetMap();
        private static OpenRouteServiceAPI openRouteServiceAPI = new OpenRouteServiceAPI();

        public static List<StationModel> allStations = jCDecauxAPI.GetStations().Result;

        public List<StationModel> GetAllStations()
        {
            return jCDecauxAPI.GetStations().Result;
        }

        public StationModel GetSpecificStation(string city, string station_number)
        {
            StationModel station = proxy.GetStationInfos(city, station_number).Result.station;
            return station;
        }

        /**
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
        **/

        public Position GetPositionOfAddress(string address)
        {
            return openStreetMap.GetPositionOfAddress(address);
        }

        public StationModel GetNearestStationFromAddress(string address)
        {
            Position p = openStreetMap.GetPositionOfAddress(address);
            //Console.WriteLine("Position of address is " + p.ToString());
            StationModel result = openStreetMap.GetNearestStationFromPosition(p);
            return result;
        }

        public List<String> GetDirections(Position[] positions)
        {
            return openRouteServiceAPI.GetDirections(positions);
        }

        public List<PathModel> GetPaths(string startAddress, string endAddress)
        {
            Position p1 = GetPositionOfAddress(startAddress);
            Position p2 = GetNearestStationFromAddress(startAddress).position;
            Position p3 = GetNearestStationFromAddress(endAddress).position;
            Position p4 = GetPositionOfAddress(endAddress);

            Position[] positions = new Position[4];
            positions[0] = p1;
            positions[1] = p2;
            positions[2] = p3;
            positions[3] = p4;
            
            List<String> paths = openRouteServiceAPI.GetDirections(positions);

            List<PathModel> result = new List<PathModel>(3);
            //Console.WriteLine("Before the PathModel parsing");
            for(int i=0; i<3; i++)
            {
                result.Add(JsonSerializer.Deserialize<PathModel>(paths[i]));
            }
            
            return result;
        }

    }
}
