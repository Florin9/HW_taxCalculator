using TaxCalculator.Data.Entities;

namespace TaxCalculator.Data.Interfaces.Repositories
{
    public interface ITaxBandCacheRepository
    {
        public Task<IEnumerable<TaxBand>> GetAllTaxBands();
    }
}
