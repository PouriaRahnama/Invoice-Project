using Invoice.Application.Dtos.InvoiceDtos;

namespace Invoice.Application.Interfaces
{
    public interface IInvoiceService
    {
        Task<Guid> CreateAsync(CreateInvoiceDto createInvoiceDto);
        Task<InvoiceDetailDto> GetByIdAsync(Guid invoiceId);      
        Task<bool> ChangeStatusAsync(Guid invoiceId, Status status);

        //Task<List<InvoiceSummaryDto>> GetMyInvoicesAsync();
    }
}
