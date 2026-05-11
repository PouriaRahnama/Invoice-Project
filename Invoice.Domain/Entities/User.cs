namespace Invoice.Domain.Entities;

/// <summary>
/// کاربر سیستم
/// </summary>
public class User : BaseEntity
{
    public User()
    {
        Customers = new List<Customer>();
        Invoices = new List<Invoice>();
    }
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public required string PasswordSalt { get; set; }

    #region Navigation
    public ICollection<Customer> Customers { get; set; } 
    public ICollection<Invoice> Invoices { get; set; } 
    #endregion
}

