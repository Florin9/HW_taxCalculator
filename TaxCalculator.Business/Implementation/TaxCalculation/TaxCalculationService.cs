using TaxCalculator.Business.Interfaces;
using TaxCalculator.Business.Interfaces.TaxCalculation;
using TaxCalculator.Data.Interfaces.Repositories;
using TaxCalculator.Domain.DTOs;

namespace TaxCalculator.Business.Implementation.TaxCalculation
{
    public class TaxCalculationService : ITaxCalculationService
    {
        private ITaxBandCacheRepository TaxBandCacheRepository { get; }
        private IRoundingService RoundingService { get; }
        private IAnnualTaxCalculatorService AnnualTaxCalculatorService { get; }

        public TaxCalculationService(
            ITaxBandCacheRepository taxBandCacheRepository,
            IRoundingService roundingService,
            IAnnualTaxCalculatorService annualTaxCalculatorService)
        {
            TaxBandCacheRepository = taxBandCacheRepository;
            RoundingService = roundingService;
            AnnualTaxCalculatorService = annualTaxCalculatorService;
        }

        public async Task<TaxCalculationResultDto> GetTaxResult(decimal annualSalary)
        {
            var taxBands = await TaxBandCacheRepository.GetAllTaxBands();

            var annualTax = AnnualTaxCalculatorService.GetAnnualTax(annualSalary, taxBands);
            annualTax = RoundingService.RoundValue(annualTax);

            return new TaxCalculationResultDto
            {
                GrossAnnualSalary = annualSalary,
                AnnualTax = annualTax,
                GrossMonthlySalary = GetRoundedMonthlyValue(annualSalary),
                MonthlyTax = GetRoundedMonthlyValue(annualTax),
                NetAnnualSalary = annualSalary - annualTax,
                NetMonthlySalary = GetRoundedMonthlyValue(annualSalary - annualTax)
            };
        }

        private decimal GetRoundedMonthlyValue(decimal value)
        {
            return RoundingService.RoundValue(value / 12);
        }
    }
}
