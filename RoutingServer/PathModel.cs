using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer
{
    internal class PathModel
    {
    }


    public class Rootobject
    {
        public Route[] routes { get; set; }
        public float[] bbox { get; set; }
        public Metadata metadata { get; set; }
    }

    public class Metadata
    {
        public string attribution { get; set; }
        public string service { get; set; }
        public long timestamp { get; set; }
        public Query query { get; set; }
        public Engine engine { get; set; }
    }

    public class Query
    {
        public float[][] coordinates { get; set; }
        public string profile { get; set; }
        public string preference { get; set; }
        public string format { get; set; }
        public string units { get; set; }
        public string language { get; set; }
        public bool instructions { get; set; }
    }

    public class Engine
    {
        public string version { get; set; }
        public DateTime build_date { get; set; }
        public DateTime graph_date { get; set; }
    }

    public class Route
    {
        public Summary summary { get; set; }
        public Segment[] segments { get; set; }
        public float[] bbox { get; set; }
        public string geometry { get; set; }
        public int[] way_points { get; set; }
    }

    public class Summary
    {
        public float distance { get; set; }
        public float duration { get; set; }
    }

    public class Segment
    {
        public float distance { get; set; }
        public float duration { get; set; }
        public Step[] steps { get; set; }
    }

    public class Step
    {
        public float distance { get; set; }
        public float duration { get; set; }
        public int type { get; set; }
        public string instruction { get; set; }
        public string name { get; set; }
        public int[] way_points { get; set; }
        public int exit_number { get; set; }
    }

}
