using CicekSepeti.Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Threading.Tasks;

namespace CicekSepeti.Infrastructure.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            try
            {
                // Redis'e bağlanmayı dene
                var jsonString = await _cache.GetStringAsync(key);

                if (string.IsNullOrEmpty(jsonString))
                    return default;

                return JsonSerializer.Deserialize<T>(jsonString);
            }
            catch
            {
              
            
         
                return default;
            }
        }

        public async Task SetAsync<T>(string key, T value, int expirationInMinutes = 60)
        {
            try
            {
                var jsonString = JsonSerializer.Serialize(value);
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expirationInMinutes)
                };
                await _cache.SetStringAsync(key, jsonString, options);
            }
            catch
            {
                // Yazarken hata olursa sessizce devam et, siteyi patlatma.
            }
        }

        public async Task RemoveAsync(string key)
        {
            try
            {
                await _cache.RemoveAsync(key);
            }
            catch
            {
                // Silerken hata olursa sessizce devam et.
            }
        }
    }
}