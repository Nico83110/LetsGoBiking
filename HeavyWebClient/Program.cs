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

            string choice = "";

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Bienvenue sur le client .Net de Let's Go Biking !");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nChoisissez ce que vous voulez faire :");
                Console.WriteLine("\t- 1. Obtenir l'itinéraire par étapes avec des vélos si possible");
                Console.WriteLine("\t- 2. Afficher les statistiques d'utilisation des appels au Proxy/Cache");
                Console.WriteLine("\t- 3. Quitter l'application .NET");
                Console.Write("\nChoix : ");

                choice = Console.ReadLine();

                if (choice.Equals("1"))
                {
                    do
                    {
                        //string startAddress = "2 Rue Marc Donadille, 13013 Marseille";
                        //string endAddress = "3 Boulevard Michelet, 13008 Marseille";

                        Console.Write("Adresse de départ : ");
                        string startAddress = Console.ReadLine();

                        Console.Write("Adresse d'arrivée : ");
                        string endAddress = Console.ReadLine();

                        Console.WriteLine("Calcul en cours de l'itinéraire de '" + startAddress + "' vers '" + endAddress + "' ...");
                        Console.WriteLine("\n\n");

                        List<PathModel> paths = wcfClient.GetPaths(startAddress, endAddress);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Voici le premier chemin à emprunter à pied : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("");
                        PrintInstructions(paths[0]);
                        Console.WriteLine("\n");

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Vous arrivez ici à la station JCDecaux de départ. Voici le chemin à vélo à suivre : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("");
                        PrintInstructions(paths[1]);
                        Console.WriteLine("\n");

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Vous arrivez ici à la station JCDecaux de fin. Voici le chemin à pied à suivre : ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("");
                        PrintInstructions(paths[2]);
                    }
                    while (Console.ReadKey().Key != ConsoleKey.Enter);
                }
                else if (choice.Equals("2"))
                {
                    do
                    {
                        Console.WriteLine("Voici les appels faits au cache pour avoir une station : \n");
                        List<Tuple<DateTime, int, double>> history = wcfClient.getHistory();
                        foreach (Tuple<DateTime, int, double> log in history)
                        {
                            Console.WriteLine('\t' + log.Item1.ToString());
                            Console.WriteLine('\t' + log.Item2.ToString());
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine('\t' + log.Item3.ToString() + " ms");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("");
                        }
                    }
                    while (Console.ReadKey().Key != ConsoleKey.Enter);

                }
                else if (choice.Equals("3"))
                {
                    Console.WriteLine("Fermeture en cours...");
                }
                else
                {
                    Console.WriteLine("Mauvaise saisie, veuillez réessayer ...");
                    Console.ReadLine();
                    //BackToMainMenu();
                }
            } while (choice.Equals("3") == false);

           /*string startAddress = "2 Rue Marc Donadille, 13013 Marseille";
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
            PrintInstructions(paths[2]); */
        }

        public static void PrintInstructions(PathModel path)
        {
            foreach (Route route in path.routes)
            {
                foreach (Segment segment in route.segments)
                {
                    foreach (Step step in segment.steps)
                    {
                        Console.WriteLine('\t' + step.instruction);
                    }
                }
            }
        }
    }
}