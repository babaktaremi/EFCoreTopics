namespace EFCoreTopics.Database.Models
{
    public class PriceHistory
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal RecordedPrice { get; set; }
        public DateTime Date { get; set; }
    }
}
