using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreTopics.Database.Models.Tpc;

public abstract class BaseOrderTpc
{
    public int Id { get; set; }

    public abstract int TableNumber { get; }

    public DateTime CreatedDate { get; set; }

    public string OrderName { get; set; }
    public string UserName { get; set; }

    protected BaseOrderTpc()
    {
        CreatedDate=DateTime.Now;
    }

    [NotMapped] public  int IncrementNumber => TableNumber * 2;
}