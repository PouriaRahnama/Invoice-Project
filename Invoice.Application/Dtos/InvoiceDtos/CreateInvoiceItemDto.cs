
namespace Invoice.Application.Dtos.InvoiceDtos
{
    public class CreateInvoiceItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal DiscountPercent { get; set; }
    }
}
