using System;
using System.ServiceModel;
//using System.ServiceModel.Web;
using RoutingServer;
using ProxyCache;
using System.Collections.Generic;

namespace HeavyWebClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenue dans le client lourd de LetsGoBiking !");

            //Default ReceivedMessageSize was not sufficient and created an error...
            BasicHttpBinding binding = new BasicHttpBinding
            {
                MaxBufferSize = 2000000,
                MaxReceivedMessageSize = 2000000
            };
            EndpointAddress endpoint = new EndpointAddress("http://localhost:8733/RoutingServer/Service1/");
            ChannelFactory<IRoutingServerService> myChannelFactory = new ChannelFactory<IRoutingServerService>(binding, endpoint);
            IRoutingServerService wcfClient = myChannelFactory.CreateChannel();

            /**
             * PRINT ALL STATIONS
            List<StationModel> stations = wcfClient.GetAllStations();
            Console.WriteLine("Voici le contenu de toutes les stations JCDecaux : ");
            foreach(StationModel station in stations)
            {
                Console.WriteLine(station.ToString());
            }
            **/

            string startAddress = "2 Rue Marc Donadille, 13013 Marseille";
            string endAddress = "3 Boulevard Michelet, 13008 Marseille";

            Console.WriteLine("Calcul en cours de l'itinéraire de '" + startAddress + "' vers '" + endAddress + "' ...");
            Console.WriteLine("\n\n");

            List<PathModel> paths = wcfClient.GetPaths(startAddress, endAddress);
            Console.WriteLine("Voici le premier chemin à emprunter à pied : ");
            Console.WriteLine("");
            PrintInstructions(paths[0]);
            Console.WriteLine("\n");
            Console.WriteLine("Vous arrivez ici à la station JCDecaux de départ. Voici le chemin à vélo à suivre : ");
            Console.WriteLine("");
            PrintInstructions(paths[1]);
            Console.WriteLine("\n");
            Console.WriteLine("Vous arrivez ici à la station JCDecaux de fin. Voici le chemin à pied à suivre : ");
            Console.WriteLine("");
            PrintInstructions(paths[2]);
        }

        public static void PrintInstructions(PathModel path)
        {
            //Console.ResetColor();
            foreach (Route route in path.routes)
            {
                foreach (Segment segment in route.segments)
                {
                    foreach (Step step in segment.steps)
                    {
                        Console.WriteLine(step.instruction);
                    }
                }
            }
        }
    }
}