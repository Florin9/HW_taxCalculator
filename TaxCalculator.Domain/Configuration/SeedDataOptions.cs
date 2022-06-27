namespace TaxCalculator.Domain.Configuration
{
    public class SeedDataOptions
    {
        public string BasePath { get; set; }
        public string DataFolder { get; set; }
        public bool ForceSeed { get; set; }
        public string FileExtension { get; set; } = ".json";
    }
}
