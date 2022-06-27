using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using TaxCalculator.Data.Entities;
using TaxCalculator.Data.Interfaces.Repositories;
using TaxCalculator.Domain.Common;

namespace TaxCalculator.Data.Implementation.Repositories
{
    public class TaxBandCacheRepository : ITaxBandCacheRepository
    {
        private readonly ITaxBandRepository _taxBandRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;


        public TaxBandCacheRepository(
            ITaxBandRepository taxBandRepository,
            IMemoryCache memoryCache,
            IConfiguration configuration)
        {
            _taxBandRepository = taxBandRepository;
            _memoryCache = memoryCache;
            _configuration = configuration;
        }

        public async Task<IEnumerable<TaxBand>> GetAllTaxBands()
        {
            var keyTimeoutSeconds = _configuration.GetValue<int>(GlobalVariables.CacheKeyTimeoutSeconds);
            if (!_memoryCache.TryGetValue(GlobalVariables.TaxBandsCacheKey, out IEnumerable<TaxBand> cacheValue))
            {
                cacheValue = await _taxBandRepository.GetAllTaxBands();

                _memoryCache.Set(GlobalVariables.TaxBandsCacheKey, cacheValue, TimeSpan.FromSeconds(keyTimeoutSeconds));
            }

            return cacheValue;
        }
    }
}
