namespace TaxCalculator.Domain.DTOs
{
    public class TaxCalculationResultDto
    {
        public decimal GrossAnnualSalary { get; set; }
        public decimal GrossMonthlySalary { get; set; }
        public decimal NetAnnualSalary { get; set; }
        public decimal NetMonthlySalary { get; set; }
        public decimal AnnualTax { get; set; }
        public decimal MonthlyTax { get; set; }
    }
}
