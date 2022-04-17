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
            EndpointAddress endpoint = new EndpointAddress("http://localhost:8733/RoutingServer/Service1/");
            ChannelFactory<IRoutingServerService> myChannelFactory = new ChannelFactory<IRoutingServerService>(binding, endpoint);
            IRoutingServerService wcfClient = myChannelFactory.CreateChannel();

            /*
            List<StationModel> stations = wcfClient.GetAllStations();
            String stationInfos = stations[0].ToString();
            Console.WriteLine("Voici le contenu de la première station : \n");
            Console.WriteLine(stationInfos);
            */

            StationModel station = wcfClient.GetSpecificStation("marseille", "9087");
            Console.WriteLine("Voici des détails de la station 9087 à Marseille :\n");
            Console.WriteLine(station.contract_name + " " + station.position);
        }
    }
}