namespace EFCoreTopics.Database.Models.Tpc;

public class InternationalOrderTpc: BaseOrderTpc
{
    public override int TableNumber => 2;

    public InternationalOrderTpc()
    {
        ShippingCode = new Random().NextInt64(1000, 10000000).ToString();
    }

    public string CountryName { get; set; }
    public string CityName { get; set; }
    public string ShippingCode { get; set; }
}