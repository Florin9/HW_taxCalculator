using Microsoft.Extensions.Configuration;
using Refit;
using TaxCalculator.IntegrationTests.Clients;

namespace TaxCalculator.IntegrationTests
{
    public class TaxCalculatorServiceFixture
    {
        public ITaxCalculatorServiceClient TaxCalculatorServiceClient { get; }
        public IConfiguration Configuration { get; }

        public TaxCalculatorServiceFixture()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            TaxCalculatorServiceClient = RestService.For<ITaxCalculatorServiceClient>(Configuration.GetValue<string>("TAX_CALCULATION_SERVICE_URL"));
        }
    }
}
