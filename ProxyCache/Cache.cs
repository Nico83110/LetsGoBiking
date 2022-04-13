using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;


namespace ProxyCache
{
    public class Cache<T> : ICache<T> where T : new()
    {
        private ObjectCache cache;
        private DateTimeOffset dt_default { get; set; }

        public Cache()
        {
            cache = MemoryCache.Default;
            dt_default = ObjectCache.InfiniteAbsoluteExpiration;
        }

        public T Get(string CacheItemName)
        {
            return (T) cache.Get(CacheItemName);
        }

        /*
        public bool Add(string key, object value, DateTimeOffset absoluteExpiration)
        {
            return cache.Add(key, value, absoluteExpiration);
        }
        */

        public T Get(string CacheItemName, DateTimeOffset dt)
        {
            //if CacheItemName doesn't exist or has a null content then create a new T object and put it in the cache with CacheItemName as the corresponding key.
            if (cache.Get(CacheItemName) == null)
            {
                CacheItemPolicy cacheItemPolicy = new CacheItemPolicy
                {
                    AbsoluteExpiration = dt
                };

                cache.Add(CacheItemName, new T(), cacheItemPolicy);
            }
            return (T)cache.Get(CacheItemName);
        }

        public T Get(string CacheItemName, double dt_seconds)
        {
            //In this case Expiration Time is now + dt_seconds seconds.
            return Get(CacheItemName, DateTimeOffset.Now.AddSeconds(dt_seconds));
        }
    }
}
