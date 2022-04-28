using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyCache
{
    public interface ICache<T>
    {
        T Get(string CacheItemName, Dictionary<string, string> infos);
        T Get(string CacheItemName, DateTimeOffset dt, Dictionary<string, string> infos);
        T Get(string CacheItemName, double dt_seconds, Dictionary<string, string> infos);
    }
}
