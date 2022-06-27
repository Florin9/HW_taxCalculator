using Microsoft.EntityFrameworkCore;
using TaxCalculator.Data;
using TaxCalculator.Data.Implementation.Repositories;
using TaxCalculator.Data.Interfaces.DataSeeder;
using TaxCalculator.Data.Interfaces.Repositories;
using TaxCalculator.Domain.Common;

namespace TaxCalculator.Web.Extensions
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(GlobalVariables.ConnectionStrings.Persistence);
            serviceCollection
                .AddDbContext<TaxCalculatorDbContext>(opt => opt
                    .UseNpgsql(connectionString)
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking))
                .AddScoped<ITaxBandRepository, TaxBandRepository>()
                .AddScoped<ITaxBandCacheRepository, TaxBandCacheRepository>();

            return serviceCollection;
        }

        public static WebApplication EnsureDbMigrationWithSeeding(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<TaxCalculatorDbContext>();
            context.Database.Migrate();

            var bootStrapService = services.GetRequiredService<IBootstrap>();
            bootStrapService.RunSeeding();

            return app;
        }
    }
}
