using FluentAssertions;
using Moq;
using TaxCalculator.Business.Implementation.TaxCalculation;
using TaxCalculator.Business.Interfaces;
using TaxCalculator.Business.Interfaces.TaxCalculation;
using TaxCalculator.Data.Entities;
using TaxCalculator.Data.Interfaces.Repositories;
using TaxCalculator.Domain.DTOs;
using Xunit;

namespace TaxCalculator.Business.Test.Implementation.TaxCalculation
{
    public class TaxCalculationServiceShould
    {
        private readonly Mock<IRoundingService> _roundingServiceMock;
        private readonly Mock<ITaxBandCacheRepository> _repositoryMock;
        private readonly Mock<IAnnualTaxCalculatorService> _annualTaxCalculatorServiceMock;

        private readonly ITaxCalculationService _service;

        private readonly IEnumerable<TaxBand> _repositoryTaxBands;

        public TaxCalculationServiceShould()
        {
            _repositoryMock = new Mock<ITaxBandCacheRepository>();
            _roundingServiceMock = new Mock<IRoundingService>();
            _annualTaxCalculatorServiceMock = new Mock<IAnnualTaxCalculatorService>();

            _service = new TaxCalculationService(
                _repositoryMock.Object,
                _roundingServiceMock.Object,
                _annualTaxCalculatorServiceMock.Object);

            _repositoryTaxBands = new List<TaxBand> { new TaxBand { TaxRate = 20, UpperLimit = 100 } };
            SetupRepositoryMock(_repositoryTaxBands);
            SetupRoundingServiceMock(1);
            SetupAnnualTaxCalculatorServiceMock(1);
        }

        private void SetupRepositoryMock(IEnumerable<TaxBand> result)
        {
            _repositoryMock
                .Setup(x => x.GetAllTaxBands())
                .ReturnsAsync(result);
        }

        private void SetupRoundingServiceMock(decimal result)
        {
            _roundingServiceMock
                .Setup(x => x.RoundValue(It.IsAny<decimal>()))
                .Returns(result);
        }

        private void SetupAnnualTaxCalculatorServiceMock(decimal result)
        {
            _annualTaxCalculatorServiceMock
                .Setup(x => x.GetAnnualTax(It.IsAny<decimal>(), It.IsAny<IEnumerable<TaxBand>>()))
                .Returns(result);
        }

        [Fact]
        public async Task GetTaxBandsFromRepository()
        {
            // arrange
            var input = 3.0m;

            // act
            var result = await _service.GetTaxResult(input);

            // assert
            _repositoryMock.Verify(x => x.GetAllTaxBands(), Times.Once);
        }

        [Fact]
        public async Task CallGetAnnualSalary()
        {
            // arrange
            var input = 3.0m;

            // act
            var result = await _service.GetTaxResult(input);

            // assert
            _annualTaxCalculatorServiceMock.Verify(x => x.GetAnnualTax(input, _repositoryTaxBands), Times.Once);
        }

        [Fact]
        public async Task CallRoundingService()
        {
            // arrange
            var input = 3.0m;

            // act
            var result = await _service.GetTaxResult(input);

            // assert
            _roundingServiceMock.Verify(x => x.RoundValue(It.IsAny<decimal>()), Times.Exactly(4));
        }

        [Fact]
        public async Task ReturnExpectedResult()
        {
            // arrange
            var input = 3.0m;
            var expectedResult = new TaxCalculationResultDto
            {
                AnnualTax = 1,
                GrossAnnualSalary = input,
                GrossMonthlySalary = 1,
                MonthlyTax = 1,
                NetAnnualSalary = 2,
                NetMonthlySalary = 1
            };

            // act
            var result = await _service.GetTaxResult(input);

            // assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
