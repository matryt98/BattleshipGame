using Microsoft.Extensions.Caching.Memory;

namespace API.Services
{
    public interface ICacheService
    {
        T? GetItemFromCache<T>(string cacheKey);
        void SetCacheItem<T>(string cacheKey, T item, DateTimeOffset? absoluteExpiration = null);
        void RemoveItemFromCache(string cacheKey);
    }

    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T? GetItemFromCache<T>(string cacheKey)
        {
            if (!_memoryCache.TryGetValue(cacheKey, out T item))
                return default;

            return item;
        }

        public void SetCacheItem<T>(string cacheKey, T item, DateTimeOffset? absoluteExpiration = null)
        {
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = absoluteExpiration,
                Priority = CacheItemPriority.Normal,
            };

            _memoryCache.Set(cacheKey, item, cacheExpiryOptions);
        }

        public void RemoveItemFromCache(string cacheKey)
        {
            _memoryCache.Remove(cacheKey);
        }
    }
}
