﻿using System;
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
    public class OpenStreetMap
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
            Console.WriteLine("openstreetmap..." + address);
            string req = "https://nominatim.openstreetmap.org/search?email=nicolas.perrin@etu.unice.fr&format=json&q=" + address; //TODO : Add e-mail to not be blocked
            try
            {
                Console.WriteLine(req);
                HttpResponseMessage response = await client.GetAsync(req);
                response.EnsureSuccessStatusCode();
                string body = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Body is : " + body);
                return JsonSerializer.Deserialize<List<Place>>(body); //TODO : Fix this line not deserialized
                Console.WriteLine("Body successfully deserialized !");
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException();
            }
        }

        public StationModel GetNearestStationFromPosition(Position p)
        {
            StationModel result = new StationModel();
            List<StationModel> allStations = RoutingServerService.allStations;
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
