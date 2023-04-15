namespace EFCoreTopics.Database.Models.Common;

public class DatabaseTransactions
{
    public DatabaseTransactions()
    {
        TransactionDate=DateTime.Now;
    }

    public Guid Id { get; set; }
    public string TableName { get; set; }
    public string OperationType { get; set; }

    public DateTime TransactionDate { get; set; }
}