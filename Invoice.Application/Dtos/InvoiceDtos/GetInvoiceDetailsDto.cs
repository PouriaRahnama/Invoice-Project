
namespace Invoice.Application.Dtos.InvoiceDtos
{
    public class GetInvoiceDetailsDto
    {
        public Guid InvoiceId { get; set; }
        public Guid CustomerId { get; set; }
        public string InvoiceNumber { get; set; }
        public string TotalPrice { get; set; }
        public string Status { get; set; }

        public string CustomerName { get; set; }
        public List<GetInvoiceItemDto> Items { get; set; } = new();
        public DateTime? CreatedDateTime { get; set; }
    }
}
