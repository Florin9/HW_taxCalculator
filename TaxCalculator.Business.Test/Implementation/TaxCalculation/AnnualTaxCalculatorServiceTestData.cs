using System.Collections;
using TaxCalculator.Data.Entities;

namespace TaxCalculator.Business.Test.Implementation.TaxCalculation
{
    public class AnnualTaxCalculatorServiceTestData : IEnumerable<object[]>
    {
        private static readonly IEnumerable<TaxBand> TaxBand = new List<TaxBand>
        {
            new TaxBand
            {
                LowerLimit = 0,
                UpperLimit = 5000,
                TaxRate = 0
            },
            new TaxBand
            {
                LowerLimit = 5000,
                UpperLimit = 20000,
                TaxRate = 20
            },
            new TaxBand
            {
                LowerLimit = 20000,
                UpperLimit = long.MaxValue,
                TaxRate = 40
            }
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 40000, 11000, TaxBand, };
            yield return new object[] { 10000, 1000, TaxBand, };
            yield return new object[] { 4000, 0, TaxBand, };
            yield return new object[] { 100000, 35000, TaxBand, };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
