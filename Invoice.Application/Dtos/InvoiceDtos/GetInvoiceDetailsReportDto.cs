
namespace Invoice.Application.Dtos.InvoiceDtos
{
    public class GetInvoiceDetailsReportDto
    {
        public Guid InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public string TotalPrice { get; set; }
        public string Status { get; set; }
        public string CustomerName { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public List<GetInvoiceItemReportDto> Items { get; set; } = new();
        public string? CreatedDateTime { get; set; }
    }
}
