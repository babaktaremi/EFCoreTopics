namespace EFCoreTopics.Database.Models
{
    public class SharedWallet
    {
        public Guid Id { get; set; }
        public string WalletName { get; set; }
        public decimal WalletAmount { get; set; }
       // public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
    }
}
