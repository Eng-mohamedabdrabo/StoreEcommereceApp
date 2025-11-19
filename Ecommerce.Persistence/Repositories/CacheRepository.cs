using Ecommerce.Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Ecommerce.Persistence.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IConnectionMultiplexer _connection;

        public CacheRepository(IConnectionMultiplexer connection)
        {
            _connection = connection;
        }

        public async Task<string?> GetAsync(string cacheKey)
        {
            var db = _connection.GetDatabase();
            var cacheValue = await db.StringGetAsync(cacheKey);
            return cacheValue.HasValue ? cacheValue.ToString() : null;
        }

        public async Task SetAsync(string cacheKey, string value, TimeSpan timeToLive)
        {
            var db = _connection.GetDatabase();
            await db.StringSetAsync(cacheKey, value, timeToLive);
        }
    }
}
