﻿using System;
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
        private static List<StationModel> stations = null; //TODO : fill here...

        public List<StationModel> GetAllStations()
        {
            return jCDecauxAPI.GetStations().Result;
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
