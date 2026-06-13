namespace Invoice.Application.GridifyMappers
{
    public class InvoiceGridifyMapper : GridifyMapper<GetInvoiceDetailsDto>
    {
        public InvoiceGridifyMapper()
        {
            AddMap("CustomerId", p => p.CustomerId);
            AddMap("InvoiceId", p => p.InvoiceId);
            AddMap("Status", p => p.Status);
            AddMap("InvoiceNumber", p => p.InvoiceNumber);
        }
    }
}
