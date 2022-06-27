namespace TaxCalculator.IntegrationTests
{
    [Collection("TaxCalculator Collection")]
    public class IntegrationTestBase : XunitContextBase
    {
        public TaxCalculatorServiceFixture Fixture { get; set; }
        public IntegrationTestBase(TaxCalculatorServiceFixture fixture, ITestOutputHelper output) : base(output)
        {
            Fixture = fixture;
        }
    }
}
