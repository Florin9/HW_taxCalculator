using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaxCalculator.Data.Entities;
using TaxCalculator.Data.Interfaces.DataSeeder;
using TaxCalculator.Domain.Common;
using TaxCalculator.Domain.Configuration;

namespace TaxCalculator.Data.Implementation.DataSeeder
{
    public class Bootstrap : IBootstrap
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConfiguration _configuration;

        public Bootstrap(IServiceScopeFactory scopeFactory, IConfiguration configuration)
        {
            _scopeFactory = scopeFactory;
            _configuration = configuration;
        }

        public void RunSeeding()
        {
            using var scope = _scopeFactory.CreateScope();
            var services = scope.ServiceProvider;

            var dbContext = services.GetRequiredService<TaxCalculatorDbContext>();
            var seedDataOptions = _configuration
                .GetSection(GlobalVariables.SeedDataOptions)
                .Get<SeedDataOptions>();

            var dataPresent = dbContext.TaxBands.Any();
            if (!dataPresent || seedDataOptions.ForceSeed)
            {
                SeedData<TaxBand>(seedDataOptions, dbContext);
            }
        }

        // todo move it to separate service
        private static void SeedData<T>(SeedDataOptions seedDataOptions, DbContext dbContext) where T : class
        {
            var str = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, seedDataOptions.BasePath,
                seedDataOptions.DataFolder, typeof(T).Name) + seedDataOptions.FileExtension);
            if (!string.IsNullOrWhiteSpace(str))
            {
                dbContext.Set<T>().RemoveRange(dbContext.Set<T>());
                var seedData = JsonSerializer.Deserialize<List<T>>(str);
                if (seedData != null)
                {
                    foreach (var taxBand in seedData)
                    {
                        dbContext.Set<T>().Add(taxBand);
                    }
                }

                dbContext.SaveChanges();
            }
        }
    }
}
