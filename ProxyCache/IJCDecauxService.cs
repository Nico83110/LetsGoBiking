using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
//using System.ServiceModel.Web;

namespace ProxyCache
{
    [ServiceContract]
    public interface IJCDecauxService
    {
        [OperationContract]
        JCDecauxItem GetStationDefault(string city, string stationNumber);

        [OperationContract]
        JCDecauxItem GetStation(string city, string stationNumber, double duration);
    }
}
