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
                Console.WriteLine("Bienvenue sur le client .Net de Let's Go Biking !");
                Console.WriteLine("\nChoisissez ce que vous voulez faire :");
                Console.WriteLine("\t- path : Établir un itinéraire entre 2 adresses avec des vélos si nécessaire");
                Console.WriteLine("\t- stats : Voir les utilisations des différentes stations de JCDecaux depuis notre serveur");
                Console.WriteLine("\t- quit : Quitter le client .Net");
                Console.Write("\nChoix : ");
                choice = Console.ReadLine().ToLower().Trim();

                if (choice.Equals("path"))
                {
                    do
                    {
                        //string startAddress = "2 Rue Marc Donadille, 13013 Marseille";
                        //string endAddress = "3 Boulevard Michelet, 13008 Marseille";

                        Console.Write("Adresse de départ : ");
                        string startAddress = Console.ReadLine(); // 11 Rue Saint Jacques, Lyon

                        Console.Write("Adresse d'arrivée : ");
                        string endAddress = Console.ReadLine(); // 19 Place Louis Pradel, Lyon OR 127 Avenue du Prado, Marseille

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
                    while (Console.ReadKey().Key != ConsoleKey.Enter);
                }
                else if (choice.Equals("stats"))
                {
                    do
                    {
                        Console.WriteLine("Voici les appels faits au cache pour avoir une station : ");
                        List<Tuple<DateTime, int, string>> history = wcfClient.getHistory();
                        foreach (Tuple<DateTime, int, string> log in history)
                        {
                            Console.WriteLine(log.Item1);
                            Console.WriteLine(log.Item2);
                            Console.WriteLine(log.Item3);
                            Console.WriteLine("");
                        }
                    }
                    while (Console.ReadKey().Key != ConsoleKey.Enter);

                    //WriteLogs(client);
                    //BackToMainMenu();
                }
                else if (choice.Equals("quit"))
                {
                    Console.WriteLine("Fermeture ...");
                }
                else
                {
                    Console.WriteLine("Mauvaise entrée, veuillez réessayer ...");
                    Console.ReadLine();
                    //BackToMainMenu();
                }
            } while (choice.Equals("quit") == false);

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