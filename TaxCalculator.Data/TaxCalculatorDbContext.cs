using Microsoft.EntityFrameworkCore;
using TaxCalculator.Data.Entities;

namespace TaxCalculator.Data
{
    public class TaxCalculatorDbContext : DbContext
    {
        public DbSet<TaxBand> TaxBands { get; set; }

        public TaxCalculatorDbContext(DbContextOptions<TaxCalculatorDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}
