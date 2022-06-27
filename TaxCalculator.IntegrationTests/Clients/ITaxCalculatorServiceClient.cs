using Refit;
using TaxCalculator.Domain.DTOs;

namespace TaxCalculator.IntegrationTests.Clients
{
    public interface ITaxCalculatorServiceClient
    {
        [Get("/TaxCalculator/{annualSalary}")]
        Task<ApiResponse<TaxCalculationResultDto>> GetTax(decimal annualSalary);
    }
}
