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

        public static List<Tuple<DateTime, int, string>> history = new List<Tuple<DateTime, int, string>>();

        public RoutingServerService() {
            //To fix CORS policy error in browser request
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public List<StationModel> GetAllStations()
        {
            Console.WriteLine("REST call for getting all stations has been received");
            return jCDecauxAPI.GetStations().Result;
        }

        public StationModel GetSpecificStation(string city, string station_number)
        {
            Console.WriteLine("Entered into the GetSpecificStation method...");
            StationModel station = JsonSerializer.Deserialize<StationModel>(proxy.GetStationInfos(city, station_number).station);
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
            return openRouteServiceAPI.GetDirections(positions, false);
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
            
            List<String> paths = openRouteServiceAPI.GetDirections(positions, false);

            List<PathModel> result = new List<PathModel>(3);
            //Console.WriteLine("Before the PathModel parsing");
            for(int i=0; i<3; i++)
            {
                result.Add(JsonSerializer.Deserialize<PathModel>(paths[i]));
            }

            
            return result;
        }

        public List<GeoJSONModel> GetPathsAsGeoJSON(string startAddress, string endAddress)
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

            List<String> paths = openRouteServiceAPI.GetDirections(positions, true);

            List<GeoJSONModel> result = new List<GeoJSONModel>(3);
            //Console.WriteLine("Before the PathModel parsing");
            for (int i = 0; i < 3; i++)
            {
                result.Add(JsonSerializer.Deserialize<GeoJSONModel>(paths[i]));
            }


            return result;
        }

        public static void addToHistory(DateTime dateTime, int stationNumber, string stationName)
        {
            Console.WriteLine("Added to logs : " + dateTime + " " + stationNumber + " " + stationName);
            history.Add(Tuple.Create(dateTime, stationNumber, stationName));
        }

        public List<Tuple<DateTime, int, string>> getHistory()
        {
            return history;
        }

    }
}
