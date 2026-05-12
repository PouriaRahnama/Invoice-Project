namespace Invoice.Domain.Entities;

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
    public long TotalPrice { get; set; }
    public int InvoiceNumber { get; set; }
    public Status Status { get; set; }

    #region Navigation
    public ICollection<InvoiceItem> Items { get; set; }
    public Customer Customer { get; set; }
    public User User { get; set; }
    #endregion
}
public enum Status
{
    Completed = 10,
    Pending = 20
}
