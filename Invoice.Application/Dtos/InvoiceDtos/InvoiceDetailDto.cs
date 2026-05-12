
namespace Invoice.Application.Dtos.InvoiceDtos
{
    public class InvoiceDetailDto
    {
        public Guid InvoiceId { get; set; }
        public int InvoiceNumber { get; set; }
        public long TotalPrice { get; set; }
        public Status Status { get; set; }

        public string CustomerName { get; set; }
        public List<InvoiceItemDto> Items { get; set; } = new();
    }
}
