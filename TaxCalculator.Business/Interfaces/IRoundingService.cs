namespace TaxCalculator.Business.Interfaces
{
    public interface IRoundingService
    {
        decimal RoundValue(decimal inputValue);
    }
}
