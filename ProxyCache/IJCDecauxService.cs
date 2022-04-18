using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ProxyCache
{
    [ServiceContract]
    public interface IJCDecauxService
    {
        [OperationContract]
        [WebGet(UriTemplate = "station?city={city}&number={station_number}")]
        JCDecauxItem GetStationDefault(string city, string station_number);

        [OperationContract]
        [WebGet]
        JCDecauxItem GetStation(string city, string stationNumber, double duration);
    }
}
