using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;


namespace ProxyCache
{
    public class Cache<T> : ICache<T>
    {
        private ObjectCache cache;

        public Cache()
        {
            cache = MemoryCache.Default;
        }

        public T Get(string CacheItemKey)
        {
            return (T) cache.Get(CacheItemKey);
        }

        public bool Add(string key, object value, DateTimeOffset absoluteExpiration)
        {
            return cache.Add(key, value, absoluteExpiration);
        }
    }
}
