
namespace Invoice.Domain.Entities
{
    /// <summary>
    /// فاکتور
    /// </summary>
    public class Invoice : BaseEntity
    {
        public Invoice()
        {
            Items = new List<InvoiceItem>();    
        }
        public Guid UserId { get; set; }  // کاربری که فاکتور را صادر کرده
        public Guid CustomerId { get; set; }
        public decimal DiscountPercent { get; set; } = 0;
        public long TotalPrice { get; set; }

        #region Navigation
        public ICollection<InvoiceItem> Items { get; set; }
        public Customer Customer { get; set; }
        public User User { get; set; }
        #endregion
    }
}
