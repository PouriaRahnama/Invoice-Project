
namespace Invoice.Application.Dtos.InvoiceDtos
{
    public class CreateInvoiceDto
    {
        public Guid CustomerId { get; set; }
        public List<CreateInvoiceItemDto> Items { get; set; } = new();
    }
}
