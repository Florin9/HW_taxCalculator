using Microsoft.AspNetCore.Mvc;

namespace TaxCalculator.Validation.Interfaces
{
    public interface IValidator<in T> where T : class
    {
        ProblemDetails? Validate(T input);
    }
}
