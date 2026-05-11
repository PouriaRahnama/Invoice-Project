namespace Invoice.Domain.Entities
{
    /// <summary>
    ///  مشتری
    /// </summary>
    public class Customer : BaseEntity
    {
        public Customer()
        {
            Invoices = new List<Invoice>();
        }
        // کسی که این مشتری را ثبت کرده 
        public Guid UserId { get; set; } 
        public required string FullName { get; set; }
        public string? Phone { get; set; } 
        public string? Address { get; set; }


        #region Navigation
        public User User { get; set; } = null!;

        // فاکتورهای صادر شده برای این مشتری
        public ICollection<Invoice> Invoices { get; set; }
        #endregion
    }
}