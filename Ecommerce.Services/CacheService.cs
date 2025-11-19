using Ecommerce.Service.Abstraction;
using Ecommerce.Domain.Contracts;
using System;
using System.Threading.Tasks;
using System.Text.Json;

namespace Ecommerce.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICacheRepository _cacheRepository;

        public CacheService(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        public async Task<string?> GetAsync(string key)
        {
            return await _cacheRepository.GetAsync(key);
        }

        public async Task SetAsync(string key, object cacheValue, TimeSpan timeToLive)
        {
            var value = JsonSerializer.Serialize(cacheValue);
            await _cacheRepository.SetAsync(key, value, timeToLive);
        }
    }
}
