using System.Net;
using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Business.Interfaces.TaxCalculation;
using TaxCalculator.Domain.DTOs;
using TaxCalculator.Validation.Interfaces;
using TaxCalculator.Validation.ValidationContext;

namespace TaxCalculator.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxCalculatorController : ControllerBase
    {
        private readonly ITaxCalculationService _taxCalculationService;
        private readonly IValidator<GetCalculatedTaxValidationContext> _validator;

        public TaxCalculatorController(
            ITaxCalculationService taxCalculationService,
            IValidator<GetCalculatedTaxValidationContext> validator)
        {
            _taxCalculationService = taxCalculationService;
            _validator = validator;
        }

        [HttpGet("{annualSalary:decimal}")]
        [ProducesResponseType(typeof(TaxCalculationResultDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCalculatedTax(decimal annualSalary)
        {
            var validationResult = _validator.Validate(
                new GetCalculatedTaxValidationContext
                    { AnnualSalary = annualSalary });
            if (validationResult != null)
            {
                return BadRequest(validationResult);
            }
            //TODO seeding mechanism for taxes - kind of done, refactoring left
            //TODO refactor startup with extensions
            var result = await _taxCalculationService.GetTaxResult(annualSalary);

            return Ok(result);
        }
    }
}
