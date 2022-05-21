namespace EFCoreTopics.Database.SqlViewModels
{
    public class GetAllCategories
    {
        public string ParentProductCategoryName { get; set; } = null!;
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; } = null!;
    }
}
