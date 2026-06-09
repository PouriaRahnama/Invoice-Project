namespace Invoice.Application.Dtos.InvoiceDtos
{
    public class FilterInvoincesDto : SearchQueryRequest
    {
        [Required(ErrorMessage = "{0} الزامی است")]
        [DisplayName("شناسه مشتری")]
        public Guid CustomerId { get; set; }
    }
}
