﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ProxyCache;
using RoutingServer.ExternalCalls;
using System.ServiceModel.Web;

namespace RoutingServer
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class RoutingServerService : IRoutingServerService
    {
        private static JCDecauxAPI jCDecauxAPI = new JCDecauxAPI();
        private static ProxyCall proxy = new ProxyCall();
        private static OpenStreetMap openStreetMap = new OpenStreetMap();

        public static List<StationModel> allStations = jCDecauxAPI.GetStations().Result;

        public List<StationModel> GetAllStations()
        {
            return jCDecauxAPI.GetStations().Result;
        }

        public StationModel GetSpecificStation(string city, string station_number)
        {
            Console.WriteLine("Calling GetSpecificStation method...");
            StationModel station = proxy.GetStationInfos(city, station_number).Result.station;
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

        public StationModel GetNearestStationFromAddress(string address)
        {
            Position p = openStreetMap.GetPositionOfAddress(address);
            Console.WriteLine("Position of address is " + p.ToString());
            StationModel result = openStreetMap.GetNearestStationFromPosition(p);
            return result;
        }
    }
}
