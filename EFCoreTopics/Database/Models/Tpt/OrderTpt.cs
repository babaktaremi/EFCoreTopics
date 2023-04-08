using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreTopics.Database.Models.Tpt;

public class OrderTpt
{
    public OrderTpt()
    {
        CreatedDate = DateTime.Now;
    }

    public int Id { get; set; }
    public string OrderName { get; set; }
    public string UserName { get; set; }
    public DateTime CreatedDate { get; set; }

   
}