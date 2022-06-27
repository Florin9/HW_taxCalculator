using TaxCalculator.Domain.DTOs;

namespace TaxCalculator.Business.Interfaces.TaxCalculation
{
    public interface ITaxCalculationService
    {
        Task<TaxCalculationResultDto> GetTaxResult(decimal annualSalary);
    }
}
