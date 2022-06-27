using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaxCalculator.Business.Interfaces.TaxCalculation;
using TaxCalculator.Domain.DTOs;
using TaxCalculator.Validation.Interfaces;
using TaxCalculator.Validation.ValidationContext;
using TaxCalculator.Web.Controllers;
using Xunit;

namespace TaxCalculator.Web.Test.Controllers
{
    public class TaxCalculatorControllerShould
    {
        private readonly Mock<IValidator<GetCalculatedTaxValidationContext>> _validatorMock;
        private readonly Mock<ITaxCalculationService> _taxCalculationServiceMock;

        private readonly TaxCalculatorController _controller;

        public TaxCalculatorControllerShould()
        {
            _validatorMock = new Mock<IValidator<GetCalculatedTaxValidationContext>>();
            _taxCalculationServiceMock = new Mock<ITaxCalculationService>();
            _controller = new TaxCalculatorController(
                _taxCalculationServiceMock.Object,
                _validatorMock.Object);

            SetupValidatorMock(null);
            SetupTaxCalculationServiceMock(new TaxCalculationResultDto());
        }
        private void SetupValidatorMock(ProblemDetails? result)
        {
            _validatorMock
                .Setup(x => x.Validate(It.IsAny<GetCalculatedTaxValidationContext>()))
                .Returns(result);
        }

        private void SetupTaxCalculationServiceMock(TaxCalculationResultDto result)
        {
            _taxCalculationServiceMock
                .Setup(x => x.GetTaxResult(It.IsAny<decimal>()))
                .ReturnsAsync(result);
        }

        [Fact]
        public async Task CallGetCalculatedTaxValidatorOnce()
        {
            // arrange
            var input = 3.0m;

            // act
            var result = await _controller.GetCalculatedTax(input);

            // assert
            _validatorMock.Verify(x => x.Validate(It.IsAny<GetCalculatedTaxValidationContext>()), Times.Once);
        }

        [Fact]
        public async Task CallGetTaxResultOnce()
        {
            // arrange
            var input = 3.0m;

            // act
            var result = await _controller.GetCalculatedTax(input);

            // assert
            _taxCalculationServiceMock.Verify(x => x.GetTaxResult(input), Times.Once);
        }

        [Fact]
        public async Task ReturnBadRequestIfValidatorReturnsProblemDetails()
        {
            // arrange
            SetupValidatorMock(new ProblemDetails());
            var input = 3.0m;

            // act
            var result = await _controller.GetCalculatedTax(input);

            // assert
            var badRequestResult = result.As<ObjectResult>();
            badRequestResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ReturnOkIfValidatorReturnsNull()
        {
            // arrange
            var input = 3.0m;
            var serviceOutput = new TaxCalculationResultDto { AnnualTax = 1 };
            SetupTaxCalculationServiceMock(serviceOutput);

            // act
            var result = await _controller.GetCalculatedTax(input);

            // assert
            var oKResult = result.As<ObjectResult>();
            oKResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            oKResult.Value.Should().Be(serviceOutput);
        }
    }
}