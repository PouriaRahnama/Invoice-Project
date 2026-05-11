namespace Invoice.Domain.Entities;
/// <summary>
/// محصول
/// </summary>
public class Product : BaseEntity
{
    public Product()
    {
        InvoiceItems = new List<InvoiceItem>();
    }
    public required string Name { get; set; }
    public required int Price { get; set; }
    public required int Quantity { get; set; }

    public ICollection<InvoiceItem> InvoiceItems { get; set; }
}

