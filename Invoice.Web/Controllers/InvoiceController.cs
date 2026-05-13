using Invoice.Application.Dtos.InvoiceDtos;
using Invoice.Domain.Entities;

namespace Invoice.Web.Controllers;
public class InvoiceController : ApiBaseController
{
    public InvoiceController(ILogger<ApiBaseController> logger) : base(logger)
    {
    }


    /// <summary>
    /// 
    /// </summary>
    [HttpPost]
    [DisplayName("")]
    public async Task<OkApiResult<Guid>> Create([FromBody] CreateInvoiceDto createInvoiceDto)
    {
        return OkApiResult<Guid>.Ok(await _invoiceService.CreateAsync(createInvoiceDto));
    }


    /// <summary>
    /// 
    /// </summary>
    [HttpGet]
    [DisplayName("")]
    public async Task<OkApiResult<GetInvoiceDetailsDto>> GetById([FromQuery] Guid invoiceId)
    {
        return OkApiResult<GetInvoiceDetailsDto>.Ok(await _invoiceService.GetByIdAsync(invoiceId));
    }

    /// <summary>
    /// 
    /// </summary>
    [HttpGet]
    [DisplayName("")]
    public async Task<OkApiResult<bool>> ChangeStatus(Guid invoiceId, Status status)
    {
        return OkApiResult<bool>.Ok(await _invoiceService.ChangeStatusAsync(invoiceId, status));
    }

}

