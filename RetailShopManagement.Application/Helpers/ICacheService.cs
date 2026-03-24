using RetailShopManagement.Domain.Entities;

namespace RetailShopManagement.Application.Helpers
{
    public interface ICacheService
    {
        /// <summary>
        /// Tries to get a cached value
        /// </summary>
        bool TryGetValue<T>(string key, out T value);

        /// <summary>
        /// Sets a value in cache with default expiration
        /// </summary>
        void Set<T>(string key, T value);

        /// <summary>
        /// Sets a value in cache with custom expiration options
        /// </summary>
        void Set<T>(string key, T value, TimeSpan absoluteExpiration, TimeSpan? slidingExpiration = null);

        /// <summary>
        /// Removes a specific cache entry
        /// </summary>
        void Remove(string key);

        /// <summary>
        /// Removes all cache entries matching a pattern
        /// </summary>
        void RemoveByPattern(string pattern);
    }

}