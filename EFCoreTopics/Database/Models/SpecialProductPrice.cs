namespace EFCoreTopics.Database.Models
{
    public class SpecialProductPrice
    {
        public int Id { get; set; }
        public SpecialProduct SpecialProduct { get; set; }
        public int SpecialProductId { get; set; }
        public decimal Price { get; set; }
        public DateTime PriceDate { get; set; }
    }
}
