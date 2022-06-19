using System.ComponentModel.DataAnnotations.Schema;
using EFCoreTopics.Database.ValueObjects;

namespace EFCoreTopics.Database.Models
{
    public class ProductPrice
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public DateTime RegisteredDate { get; set; }
        public Money Money { get; set; }


        /// <summary>
        /// Default Constructor that client developers use
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productName"></param>
        /// <param name="registeredDate"></param>
        /// <param name="money"></param>
        public ProductPrice(Guid id, string productName, DateTime registeredDate, Money money)
        {
            Id = id;
            ProductName = productName;
            RegisteredDate = registeredDate;
            Money = money;
        }

        /// <summary>
        /// For EF Core to understand the object creation
        /// </summary>
        private ProductPrice()
        {
            
        }
    }
}
