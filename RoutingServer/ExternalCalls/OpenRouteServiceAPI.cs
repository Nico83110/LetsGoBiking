using ProxyCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer.ExternalCalls
{
    public class OpenRouteServiceAPI
    {
        private HttpClient client = new HttpClient();
        private string apiKey = "5b3ce3597851110001cf6248386a163bffd64098bce61b52dfc5ed87";

        public OpenRouteServiceAPI()
        {
        }

        public async Task<string> CallDirectionsServicePOST(string uri, string postBody)
        {
            try
            {
                StringContent content = new StringContent(postBody, Encoding.UTF8, "application/json");
                HttpRequestMessage httpRequest = new HttpRequestMessage()
                {
                    RequestUri = new Uri(uri),
                    Method = HttpMethod.Post,
                    Content = content
                };

                //Mandatory for every POST Request on OpenRouteService
                httpRequest.Headers.Add("Authorization", apiKey);

                HttpResponseMessage response = await client.SendAsync(httpRequest);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException();
            }
        }

        /* positions has to respectively contain the start address, nearest bike station, end bike station, end address
         The result is a list containing respectively the three paths (walking, cycling, walking) to take.
         */
        public List<String> GetDirections(Position[] positions, bool asGeoJSON)
        {
                Position startPos = positions[0];
                Position nearestStation = positions[1];
                Position endStation = positions[2];
                Position endPos = positions[3];
            

            string uri = "https://api.openrouteservice.org/v2/directions/";
            string postBody1, postBody2, postBody3;
            string additionals = "],\"instructions\":\"true\",\"language\":\"fr\",\"preference\":\"shortest\",\"units\":\"m\"}";

            /** First Path Query Preparation **/
            postBody1 = "{\"coordinates\":[" + "[" + startPos.longitude.ToString().Replace(",", ".") + "," + startPos.latitude.ToString().Replace(",", ".") + "],"
                + "[" + nearestStation.longitude.ToString().Replace(",", ".") + "," + nearestStation.latitude.ToString().Replace(",", ".") + "],";
            postBody1 += additionals;
            postBody1 = postBody1.Replace("],],", "]],");

            /** Second Path Query Preparation **/
            postBody2 = "{\"coordinates\":[" + "[" + nearestStation.longitude.ToString().Replace(",", ".") + "," + nearestStation.latitude.ToString().Replace(",", ".") + "],"
                + "[" + endStation.longitude.ToString().Replace(",", ".") + "," + endStation.latitude.ToString().Replace(",", ".") + "],";
            postBody2 += additionals;
            postBody2 = postBody2.Replace("],],", "]],");

            /** Third Path Query Preparation **/
            postBody3 = "{\"coordinates\":[" + "[" + endStation.longitude.ToString().Replace(",", ".") + "," + endStation.latitude.ToString().Replace(",", ".") + "],"
                + "[" + endPos.longitude.ToString().Replace(",", ".") + "," + endPos.latitude.ToString().Replace(",", ".") + "],";
            postBody3 += additionals;
            postBody3 = postBody3.Replace("],],", "]],");

            Console.WriteLine("Request url is : " + uri + "\n and body is : " + postBody1);

            //TODO : Fix calls under that
            string firstPath = CallDirectionsServicePOST(uri + "foot-walking" + (asGeoJSON ? "/geojson" : ""), postBody1).Result;
            string secondPath = CallDirectionsServicePOST(uri + "cycling-regular" + (asGeoJSON ? "/geojson" : ""), postBody2).Result;
            string thirdPath = CallDirectionsServicePOST(uri + "foot-walking" + (asGeoJSON ? "/geojson" : ""), postBody3).Result;

            List<String> paths = new List<string>();
            paths.Add(firstPath);
            paths.Add(secondPath);
            paths.Add(thirdPath);

            return paths;
        }


    }

}
