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

            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress endpoint = new EndpointAddress("http://localhost:8733/RoutingServer/Service1/");
            ChannelFactory<IRoutingServerService> myChannelFactory = new ChannelFactory<IRoutingServerService>(binding, endpoint);
            IRoutingServerService wcfClient = myChannelFactory.CreateChannel();

            List<StationModel> stations = wcfClient.GetAllStations();
            Console.WriteLine("Voici le contenu de la liste de toutes les stations : \n");
            Console.WriteLine(stations);
        }
    }
}