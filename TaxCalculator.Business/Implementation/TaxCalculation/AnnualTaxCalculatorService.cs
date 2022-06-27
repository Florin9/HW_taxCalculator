using TaxCalculator.Business.Interfaces.TaxCalculation;
using TaxCalculator.Data.Entities;

namespace TaxCalculator.Business.Implementation.TaxCalculation
{
    public class AnnualTaxCalculatorService : IAnnualTaxCalculatorService
    {
        public decimal GetAnnualTax(decimal annualSalary, IEnumerable<TaxBand> taxBands)
        {
            var totalTax = 0m;

            foreach (var band in taxBands)
            {
                if (annualSalary > band.LowerLimit)
                {
                    var taxableAtThisRate = Math.Min(band.UpperLimit - band.LowerLimit, annualSalary - band.LowerLimit);
                    var taxThisBand = taxableAtThisRate * band.TaxRate / 100;
                    totalTax += taxThisBand;
                }
            }

            return totalTax;
        }
    }
}
