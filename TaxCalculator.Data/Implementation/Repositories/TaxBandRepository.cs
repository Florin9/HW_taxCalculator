using Microsoft.EntityFrameworkCore;
using TaxCalculator.Data.Entities;
using TaxCalculator.Data.Interfaces.Repositories;

namespace TaxCalculator.Data.Implementation.Repositories
{
    public class TaxBandRepository : ITaxBandRepository
    {
        private TaxCalculatorDbContext DbContext { get; }

        public TaxBandRepository(TaxCalculatorDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<IEnumerable<TaxBand>> GetAllTaxBands()
        {
            return await DbContext.TaxBands.ToListAsync();
        }
    }
}
