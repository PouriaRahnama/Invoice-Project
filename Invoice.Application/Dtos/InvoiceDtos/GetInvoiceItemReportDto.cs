
namespace Invoice.Application.Dtos.InvoiceDtos
{
    public class GetInvoiceItemReportDto
    {
        public Guid InvoiceId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string UnitPrice { get; set; }
        public string DiscountPercent { get; set; }
        public string TotalPrice { get; set; }
    }
}
