namespace TaxCalculator.Data.Interfaces
{
    public interface IEntity<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
