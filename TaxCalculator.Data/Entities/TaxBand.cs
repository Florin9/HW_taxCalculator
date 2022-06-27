using TaxCalculator.Data.Interfaces;

namespace TaxCalculator.Data.Entities
{
    public class TaxBand : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public long LowerLimit { get; set; }
        public long UpperLimit { get; set; }
        public int TaxRate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
