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
    [AllowAnonymous]
    [DisplayName("ایجاد فاکتور")]
    public async Task<OkApiResult<Guid>> Create([FromBody] CreateInvoiceDto createInvoiceDto)
    {
        return OkApiResult<Guid>.Ok(await _invoiceService.CreateAsync(createInvoiceDto));
    }


    /// <summary>
    /// واکشی فاکتور توسط شناسه
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [DisplayName("واکشی فاکتور توسط شناسه")]
    public async Task<OkApiResult<GetInvoiceDetailsDto>> GetById([FromQuery] Guid invoiceId)
    {
        return OkApiResult<GetInvoiceDetailsDto>.Ok(await _invoiceService.GetByIdAsync(invoiceId));
    }

    /// <summary>
    /// واکشی فاکتورها با فیلتر - شناسه مشتری
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [DisplayName("واکشی فاکتورها با فیلتر - شناسه مشتری")]
    public async Task<OkApiResult<SearchQueryResponse<GetInvoiceDetailsDto>>> GetAll([FromQuery] FilterInvoincesDto QueryParams)
    {
        return OkApiResult<SearchQueryResponse<GetInvoiceDetailsDto>>.Ok(await _invoiceService.GetAllAsync(QueryParams));
    }

    /// <summary>
    /// تغییر وضعیت فاکتور توسط شناسه فاکتور 
    /// </summary>
    [HttpPost]
    [AllowAnonymous]
    [DisplayName(" تغییر وضعیت فاکتور توسط شناسه فاکتور ")]
    public async Task<OkApiResult<bool>> ChangeStatus(Guid invoiceId, Status status)
    {
        return OkApiResult<bool>.Ok(await _invoiceService.ChangeStatusAsync(invoiceId, status));
    }

}

