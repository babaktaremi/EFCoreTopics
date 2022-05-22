namespace EFCoreTopics.Database.QueryModels
{
    public class GetCityAndProvinceFromAddressModel
    {
        public int Id { get; set; }
        public string City { get; set; } = null!;
        public string StateProvince { get; set; }= null!;
    }
}
