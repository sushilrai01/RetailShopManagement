using System.Collections.Concurrent;
using Microsoft.Extensions.Caching.Memory;
using RetailShopManagement.Application.Helpers;

namespace RetailShopManagement.Infrastructure.Helpers
{
    public class CacheService(IMemoryCache memoryCache) : ICacheService
    {
        private readonly ConcurrentDictionary<string, byte> _cacheKeys = new();
        private static readonly TimeSpan DefaultAbsoluteExpiration = TimeSpan.FromHours(24);
        private static readonly TimeSpan DefaultSlidingExpiration = TimeSpan.FromMinutes(5);

        public bool TryGetValue<T>(string key, out T value)
        {
            return memoryCache.TryGetValue(key, out value);
        }

        public void Set<T>(string key, T value)
        {
            Set(key, value, DefaultAbsoluteExpiration, DefaultSlidingExpiration);
        }

        public void Set<T>(string key, T value, TimeSpan absoluteExpiration, TimeSpan? slidingExpiration = null)
        {
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(absoluteExpiration)
                .RegisterPostEvictionCallback((evictedKey, evictedValue, reason, state) =>
                {
                    // Remove from tracking when evicted
                    _cacheKeys.TryRemove(evictedKey.ToString(), out _);
                });

            if (slidingExpiration.HasValue)
            {
                cacheOptions.SetSlidingExpiration(slidingExpiration.Value);
            }

            memoryCache.Set(key, value, cacheOptions);
            _cacheKeys.TryAdd(key, 0);
        }

        public void Remove(string key)
        {
            memoryCache.Remove(key);
            _cacheKeys.TryRemove(key, out _);
        }

        public void RemoveByPattern(string pattern)
        {
            var keysToRemove = _cacheKeys.Keys
                .Where(k => k.Contains(pattern))
                .ToList();

            foreach (var key in keysToRemove)
            {
                Remove(key);
            }
        }
    }
}