using EFCoreTopics.Database.Models.Tph;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreTopics.Database.Models.Tpt;

public class InternationalOrderTpt : OrderTpt
{
    public InternationalOrderTpt()
    {
        ShippingCode = new Random().NextInt64(1000, 10000000).ToString();
    }

    public string CountryName { get; set; }
    public string CityName { get; set; }
    public string ShippingCode { get; set; }

   
}