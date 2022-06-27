using FluentAssertions;
using TaxCalculator.Validation.InputValidators;
using TaxCalculator.Validation.Interfaces;
using TaxCalculator.Validation.ValidationContext;
using Xunit;

namespace TaxCalculator.Validation.Test.InputValidators
{
    public class GetCalculatedTaxValidatorShould
    {
        private readonly IValidator<GetCalculatedTaxValidationContext> _validator;

        public GetCalculatedTaxValidatorShould()
        {
            _validator = new GetCalculatedTaxValidator();
        }

        [Fact]
        public void ReturnProblemDetailsWhenInputIsInvalid()
        {
            //arrange
            var salary = decimal.MinValue;
            var validationInput = new GetCalculatedTaxValidationContext
            {
                AnnualSalary = salary
            };
            // act
            var result = _validator.Validate(validationInput);

            // assert
            result.Should().NotBeNull();
            result.Type.Should().Be("WRONG_INPUT");
            result.Detail.Should().Be("Input salary can't be negative");
            result.Title.Should().Be("Negative Input Salary");
        }

        [Theory]
        [InlineData(10)]
        [InlineData(10000000)]
        [InlineData(1)]
        [InlineData(0)]
        public void ReturnNullWhenInputIsValid(decimal salary)
        {
            //arrange
            var validationInput = new GetCalculatedTaxValidationContext
            {
                AnnualSalary = salary
            };
            // act
            var result = _validator.Validate(validationInput);

            // assert
            result.Should().BeNull();
        }
    }
}
