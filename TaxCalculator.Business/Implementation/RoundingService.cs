using Microsoft.Extensions.Configuration;
using TaxCalculator.Business.Interfaces;
using TaxCalculator.Domain.Common;

namespace TaxCalculator.Business.Implementation
{
    public class RoundingService : IRoundingService
    {
        private readonly IConfiguration _configuration;

        public RoundingService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public decimal RoundValue(decimal inputValue)
        {
            var roundingDecimals = _configuration.GetValue<int>(GlobalVariables.RoundingDecimals);

            return Math.Round(inputValue, roundingDecimals, MidpointRounding.AwayFromZero);
        }
    }
}
