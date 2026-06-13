namespace Invoice.Application.Interfaces
{
    public interface IInvoiceService
    {
        Task<Guid> CreateAsync(CreateInvoiceDto createInvoiceDto);
        Task<GetInvoiceDetailsDto> GetByIdAsync(Guid invoiceId);      
        Task<bool> ChangeStatusAsync(Guid invoiceId, Status status);
        Task<SearchQueryResponse<GetInvoiceDetailsDto>> GetAllAsync(FilterInvoincesDto QueryParams);
    }
}
