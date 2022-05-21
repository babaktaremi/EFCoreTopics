namespace EFCoreTopics.Database.FunctionModels
{
    public class GetCustomerModel
    {
        public int CustomerId { get; set; } 
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
