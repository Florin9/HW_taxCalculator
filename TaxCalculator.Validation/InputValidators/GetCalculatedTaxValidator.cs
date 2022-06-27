using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaxCalculator.Validation.Interfaces;
using TaxCalculator.Validation.ValidationContext;

namespace TaxCalculator.Validation.InputValidators
{
    public class GetCalculatedTaxValidator : IValidator<GetCalculatedTaxValidationContext>
    {
        public ProblemDetails? Validate(GetCalculatedTaxValidationContext input)
        {
            if (input.AnnualSalary < 0)
            {
                return new ProblemDetails
                {
                    Detail = "Input salary can't be negative",
                    Title = "Negative Input Salary",
                    Type = "WRONG_INPUT",
                    Status = (int)HttpStatusCode.BadRequest
                };
            }

            return null;
        }
    }
}
