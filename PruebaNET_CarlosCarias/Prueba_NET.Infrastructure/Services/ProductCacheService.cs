using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_NET.Infrastructure.Services
{
    public class ProductCacheService
    {
        private readonly IMemoryCache _cache;
        private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(5);

        public ProductCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Dictionary<int, string> GetStatusDictionary()
        {
            if(!_cache.TryGetValue("ProductStatus", out Dictionary<int, string> statusDictionary))
            {
                statusDictionary = new Dictionary<int, string>()
                {
                    {1, "Active" },
                    {0, "Inactive" }
                };
                _cache.Set("ProductStatus", statusDictionary, CacheDuration);
            }

            return statusDictionary;
        }
    }
}
