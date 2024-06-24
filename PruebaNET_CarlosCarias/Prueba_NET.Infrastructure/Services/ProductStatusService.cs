using Microsoft.Extensions.Caching.Memory;
using Prueba_NET.Application.Interfaces;
using Prueba_NET.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_NET.Infrastructure.Services
{
    public class ProductStatusService : IProductStatusService
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);
        private readonly ApplicationDbContext _dbContext;

        public ProductStatusService(IMemoryCache cache, ApplicationDbContext dbContext)
        {
            _cache = cache;
            _dbContext = dbContext;
        }

        public Dictionary<int, string> GetProductStatuses()
        {
            return _cache.GetOrCreate("ProductStatuses", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _cacheDuration;
                return LoadProductStatuses();
            });
        }

        private Dictionary<int, string> LoadProductStatuses()
        {
            return _dbContext.ProductStatuses.ToDictionary(ps => ps.StatusId, ps => ps.StatusName);
        }
    }

}
