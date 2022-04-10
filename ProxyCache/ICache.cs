using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyCache
{
    public interface ICache<T>
    {
        T Get(string CacheItem);
    }
}
