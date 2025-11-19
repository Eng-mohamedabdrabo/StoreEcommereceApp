using System;
using System.Threading.Tasks;

namespace Ecommerce.Service.Abstraction
{
    public interface ICacheService
    {
        Task<string?> GetAsync(string key);
        Task SetAsync(string key, object cacheValue, TimeSpan timeToLive);
        
    }
}