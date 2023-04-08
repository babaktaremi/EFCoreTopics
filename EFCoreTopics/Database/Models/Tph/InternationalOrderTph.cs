namespace EFCoreTopics.Database.Models.Tph;

public class InternationalOrderTph:OrderTph
{
    public InternationalOrderTph()
    {
        ShippingCode = new Random().NextInt64(1000, 10000000).ToString();
    }

    public string CountryName { get; set; }
    public string CityName { get; set; }
    public string ShippingCode { get; set; }
}