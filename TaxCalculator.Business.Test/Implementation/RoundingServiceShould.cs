using FluentAssertions;
using Microsoft.Extensions.Configuration;
using TaxCalculator.Business.Implementation;
using TaxCalculator.Business.Interfaces;
using TaxCalculator.Domain.Common;
using Xunit;

namespace TaxCalculator.Business.Test.Implementation
{
    public class RoundingServiceShould
    {
        private readonly IConfiguration _configuration;
        private readonly IRoundingService _service;

        public RoundingServiceShould()
        {
            var inMemory = new Dictionary<string, string>
            {
                { GlobalVariables.RoundingDecimals, "2" },
            };
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemory)
                .Build();

            _service = new RoundingService(_configuration);
        }

        [Theory]
        [InlineData(2.456, 2.46)]
        [InlineData(232.8943, 232.89)]
        [InlineData(42.78654, 42.79)]
        public void ReturnRoundedValue(decimal input, decimal expected)
        {
            // act
            var result = _service.RoundValue(input);

            // assert
            result.Should().Be(expected);
        }
    }
}