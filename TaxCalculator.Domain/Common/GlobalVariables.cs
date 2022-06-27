namespace TaxCalculator.Domain.Common
{
    public static class GlobalVariables
    {
        public static class ConnectionStrings
        {
            public static string Persistence = "Persistence";
        }

        public static string SeedDataOptions = "SeedDataOptions";
        public static string RoundingDecimals = "RoundingDecimals";
        public static string CacheKeyTimeoutSeconds = "CacheKeyTimeoutSeconds";
        public static string TaxBandsCacheKey = "taxBands";
    }
}
