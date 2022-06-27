using TaxCalculator.Data.Entities;

namespace TaxCalculator.Business.Interfaces.TaxCalculation
{
    public interface IAnnualTaxCalculatorService
    {
        decimal GetAnnualTax(decimal annualSalary, IEnumerable<TaxBand> taxBands);
    }
}
