
namespace Invoice.Application.Dtos.InvoiceDtos
{
    public class InvoiceItemDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public long UnitPrice { get; set; }
        public decimal DiscountPercent { get; set; }
        public long TotalPrice { get; set; }
    }
}
