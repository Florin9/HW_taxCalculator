using TaxCalculator.Data.Entities;

namespace TaxCalculator.Data.Interfaces.Repositories
{
    public interface ITaxBandRepository
    {
        Task<IEnumerable<TaxBand>> GetAllTaxBands();
    }
}
