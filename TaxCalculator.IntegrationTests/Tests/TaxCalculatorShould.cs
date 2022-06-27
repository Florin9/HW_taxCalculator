using System.Net;
using FluentAssertions;
using TaxCalculator.Domain.DTOs;
using TaxCalculator.IntegrationTests.TestData;

namespace TaxCalculator.IntegrationTests.Tests
{
    public class TaxCalculatorShould : IntegrationTestBase
    {
        public TaxCalculatorShould(TaxCalculatorServiceFixture fixture, ITestOutputHelper output) : base(fixture, output)
        {
        }

        [Theory]
        [ClassData(typeof(TaxCalculatorTestData))]
        public async Task ReturnBadRequestForInvalidInput(decimal input)
        {
            var response = await Fixture.TaxCalculatorServiceClient.GetTax(input);

            // assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ReturnExpectedResponseForValidInput()
        {
            // arrange
            var input = 40000;
            var expectedResponse = new TaxCalculationResultDto
            {
                AnnualTax = 11000,
                GrossAnnualSalary = 40000,
                GrossMonthlySalary = 3333.33m,
                MonthlyTax = 916.67m,
                NetAnnualSalary = 29000,
                NetMonthlySalary = 2416.67m
            };
            // act
            var response = await Fixture.TaxCalculatorServiceClient.GetTax(input);

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().BeEquivalentTo(expectedResponse);
        }
    }
}
