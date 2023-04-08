namespace EFCoreTopics.Database.Models.Tph;

public class OrderTph
{
    public OrderTph()
    {
        CreatedDate = DateTime.Now;
    }

    public int Id { get; set; }
    public string OrderName { get; set; }
    public string UserName { get; set; }
    public DateTime CreatedDate { get; set; }
}