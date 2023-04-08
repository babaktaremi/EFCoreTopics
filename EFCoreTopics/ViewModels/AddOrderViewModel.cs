namespace EFCoreTopics.ViewModels;

public record AddOrderViewModel(string OrderName,string UserName,string CityName="",string CountryName="",bool IsInternational=false);