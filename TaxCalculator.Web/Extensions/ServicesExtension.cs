using TaxCalculator.Business.Implementation;
using TaxCalculator.Business.Implementation.TaxCalculation;
using TaxCalculator.Business.Interfaces;
using TaxCalculator.Business.Interfaces.TaxCalculation;
using TaxCalculator.Data.Implementation.DataSeeder;
using TaxCalculator.Data.Interfaces.DataSeeder;
using TaxCalculator.Validation.InputValidators;
using TaxCalculator.Validation.Interfaces;
using TaxCalculator.Validation.ValidationContext;

namespace TaxCalculator.Web.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddScoped<ITaxCalculationService, TaxCalculationService>()
                .AddScoped<IRoundingService, RoundingService>()
                .AddScoped<IAnnualTaxCalculatorService, AnnualTaxCalculatorService>()
                .AddScoped<IBootstrap, Bootstrap>();

            return serviceCollection;
        }

        public static IServiceCollection AddValidators(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddScoped<IValidator<GetCalculatedTaxValidationContext>, GetCalculatedTaxValidator>();

            return serviceCollection;
        }
    }
}
