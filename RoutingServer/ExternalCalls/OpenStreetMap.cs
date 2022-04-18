using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using ProxyCache;
using System.Runtime.Serialization;
using System.Text.Json;
using RoutingServer;
using System.Device.Location;

namespace RoutingServer.ExternalCalls
{
    internal class OpenStreetMap
    {
        private static HttpClient client = new HttpClient();

        public OpenStreetMap()
        {
        }
        public Position GetPositionOfAddress(string address)
        {
            List<Place> places = GetPlacesNearAdress(address).Result;
            float score = 0;
            Place nearestPlace = null;

            foreach(Place place in places)
            {
                if(place.importance > score)
                {
                    score = place.importance;
                    nearestPlace = place;
                }
            }

            Position result = new Position
            {
                latitude = float.Parse(nearestPlace.lat),
                longitude = float.Parse(nearestPlace.lon)
            };

            return result;
        }
        public async Task<List<Place>> GetPlacesNearAdress(string address)
        {
            string req = "https://nominatim.openstreetmap.org/search?format=json&q=" + address;
            try
            {
                HttpResponseMessage response = await client.GetAsync(req);
                response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<List<Place>>(body);
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public StationModel GetNearestStationFromPosition(Position p)
        {
            StationModel result = new StationModel();
            RoutingServerService rss = new RoutingServerService();
            List<StationModel> allStations = rss.GetAllStations();
            GeoCoordinate currentGeoP = new GeoCoordinate(p.latitude, p.longitude);
            double distance = 100000000000; //No distance should be superior to that hopefully

            foreach(StationModel station in allStations)
            {
                GeoCoordinate stationGeoP = new GeoCoordinate(station.position.latitude, station.position.longitude);

                if(currentGeoP.GetDistanceTo(stationGeoP) <= distance)
                {
                    result = station;
                    distance = currentGeoP.GetDistanceTo(stationGeoP);
                }
            }
            return result;
        }


    }

    [DataContract]
    public class Place
    {
        public Place()
        {
        }

        [DataMember]
        public float importance { get; set; }
        [DataMember]
        public string lat { get; set; }
        [DataMember]

        public string lon { get; set; }

    }
}
