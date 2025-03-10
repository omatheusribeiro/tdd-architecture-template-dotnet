using Microsoft.Extensions.Caching.Memory;

namespace tdd_architecture_template_dotnet.Infrastructure.Singleton.Cache
{
    public class CacheService
    {
        private readonly MemoryCache _cache;

        public CacheService()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        public T? Get<T>(string key)
        {
            _cache.TryGetValue(key, out T value);
            return value;
        }

        public void Set<T>(string key, T value, TimeSpan expiration)
        {
            _cache.Set(key, value, expiration);
        }
    }

}
