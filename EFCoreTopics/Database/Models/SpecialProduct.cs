namespace EFCoreTopics.Database.Models
{
    public class SpecialProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<SpecialProductPrice> SpecialProductPrices { get; set; }
    }
}
