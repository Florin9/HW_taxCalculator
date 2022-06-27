using FluentAssertions;
using TaxCalculator.Business.Implementation.TaxCalculation;
using TaxCalculator.Data.Entities;
using Xunit;

namespace TaxCalculator.Business.Test.Implementation.TaxCalculation
{
    public class AnnualTaxCalculatorServiceShould
    {
        private readonly AnnualTaxCalculatorService _service;

        public AnnualTaxCalculatorServiceShould()
        {
            _service = new AnnualTaxCalculatorService();
        }

        [Theory]
        [ClassData(typeof(AnnualTaxCalculatorServiceTestData))]
        public void ReturnCorrectAnnualTax(decimal annualSalary, decimal expectedTax, IEnumerable<TaxBand> taxBands)
        {
            // act
            var result = _service.GetAnnualTax(annualSalary, taxBands);

            // assert
            result.Should().Be(expectedTax);
        }
    }
}
