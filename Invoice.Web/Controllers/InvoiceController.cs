namespace Invoice.Web.Controllers;
public class InvoiceController : ApiBaseController
{
    public InvoiceController(ILogger<ApiBaseController> logger) : base(logger)
    {
    }


    /// <summary>
    /// ایجاد فاکتور
    /// </summary>
    [HttpPost]
    [DisplayName("ایجاد فاکتور")]
    public async Task<OkApiResult<Guid>> Create([FromBody] CreateInvoiceDto createInvoiceDto)
    {
        return OkApiResult<Guid>.Ok(await _invoiceService.CreateAsync(createInvoiceDto));
    }


    /// <summary>
    /// واکشی فاکتور توسط شناسه
    /// </summary>
    [HttpGet]
    [DisplayName("واکشی فاکتور توسط شناسه")]
    public async Task<OkApiResult<GetInvoiceDetailsDto>> GetById([FromQuery] Guid invoiceId)
    {
        return OkApiResult<GetInvoiceDetailsDto>.Ok(await _invoiceService.GetByIdAsync(invoiceId));
    }

    /// <summary>
    /// واکشی فاکتورها توسط شناسه مشتری
    /// </summary>
    [HttpGet]
    [DisplayName("واکشی فاکتورها توسط شناسه مشتری")]
    public async Task<OkApiResult<IEnumerable<GetInvoiceDetailsDto>>> GetByCustomerId([FromQuery] Guid customerId)
    {
        return OkApiResult<IEnumerable<GetInvoiceDetailsDto>>.Ok(await _invoiceService.GetByCustomerIdAsync(customerId));
    }

    /// <summary>
    /// تغییر وضعیت فاکتور توسط شناسه فاکتور 
    /// </summary>
    [HttpPost]
    [DisplayName(" تغییر وضعیت فاکتور توسط شناسه فاکتور ")]
    public async Task<OkApiResult<bool>> ChangeStatus(Guid invoiceId, Status status)
    {
        return OkApiResult<bool>.Ok(await _invoiceService.ChangeStatusAsync(invoiceId, status));
    }

}

