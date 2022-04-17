using System;
using System.ServiceModel;
using RoutingServer;
using ProxyCache;

namespace HeavyWebClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello from the Heavy Client !");

            //Default ReceivedMessageSize was not sufficient and created an error...
            BasicHttpBinding binding = new BasicHttpBinding
            {
                MaxBufferSize = 2000000,
                MaxReceivedMessageSize = 2000000
            };
            EndpointAddress endpoint = new EndpointAddress("http://localhost:8733/ProxyCache/Service1/");
            ChannelFactory<IJCDecauxService> myChannelFactory = new ChannelFactory<IJCDecauxService>(binding, endpoint);
            IJCDecauxService wcfClient = myChannelFactory.CreateChannel();

            /*
            List<StationModel> stations = wcfClient.GetAllStations();
            String stationInfos = stations[0].ToString();
            Console.WriteLine("Voici le contenu de la première station : \n");
            Console.WriteLine(stationInfos);
            */

            StationModel station = wcfClient.GetStationDefault("marseille", "9087").station;
            Console.WriteLine("Voici des détails de la station 9087 à Marseille :\n");
            Console.WriteLine(station.contract_name + " " + station.position);
        }
    }
}