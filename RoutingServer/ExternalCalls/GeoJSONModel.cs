using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer
{

        public class GeoJSONModel
        {
            public string type { get; set; }
            public Feature[] features { get; set; }
            public float[] bbox { get; set; }
            public Metadata metadata { get; set; }
        }

        public class Feature
        {
            public float[] bbox { get; set; }
            public string type { get; set; }
            public Properties properties { get; set; }
            public Geometry geometry { get; set; }
        }

        public class Properties
        {
            public Segment[] segments { get; set; }
            public Summary summary { get; set; }
            public int[] way_points { get; set; }
        }


        public class Geometry
        {
            public float[][] coordinates { get; set; }
            public string type { get; set; }
        }

    
}
