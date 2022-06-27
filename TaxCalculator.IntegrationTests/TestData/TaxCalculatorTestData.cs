using System.Collections;

namespace TaxCalculator.IntegrationTests.TestData
{
    public class TaxCalculatorTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { -1m };
            yield return new object[] { -12321321m };
            yield return new object[] { decimal.MinValue };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
