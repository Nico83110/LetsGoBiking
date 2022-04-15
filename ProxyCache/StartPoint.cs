using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ProxyCache
{
    internal class StartPoint
    {
        // Host the service within this EXE console application.
        public static void Main()
        {
            // Create a ServiceHost for the CalculatorService type.
            using (
                ServiceHost serviceHost =
                new ServiceHost(typeof(JCDecauxService)))
            {
                // Open the ServiceHost to create listeners
                // and start listening for messages.
                serviceHost.Open();

                // The service can now be accessed.
                Console.WriteLine("Le service RoutingServer est prêt à l'adresse : {0}", serviceHost.BaseAddresses[0]);
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();
                Console.ReadLine();
            }
        }
    }
}
