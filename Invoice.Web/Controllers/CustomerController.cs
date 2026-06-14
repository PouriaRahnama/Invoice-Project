namespace Invoice.Web.Controllers
{
    public class CustomerController : ApiBaseController
    {
        public CustomerController(ILogger<ApiBaseController> logger) : base(logger)
        {
        }
        /// <summary>
        /// واکشی مشتریان - واکشی مشتریان ثبت شده توسط کاربر سیستم
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [DisplayName("واکشی مشتریان - واکشی مشتریان ثبت شده توسط کاربر سیستم")]
        public async Task<OkApiResult<SearchQueryResponse<GetAllCustomersDto>>> GetAll([FromQuery] FilterCustomersDto QueryParams)
        {
            return OkApiResult<SearchQueryResponse<GetAllCustomersDto>>.Ok(await _customerService.GetAllAsync(QueryParams));
        }

        /// <summary>
        /// واکشی مشتریان (کلی) - واکشی مشتریان ثبت شده توسط کاربر سیستم
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [DisplayName("واکشی مشتریان (کلی) - واکشی مشتریان ثبت شده توسط کاربر سیستم")]
        public async Task<OkApiResult<SearchQueryResponse<GetCustomersDto>>> GetCustomers([FromQuery] FilterCustomersDto QueryParams)
        {
            return OkApiResult<SearchQueryResponse<GetCustomersDto>>.Ok(await _customerService.GetCustomersAsync(QueryParams));
        }

        /// <summary>
        /// واکشی مشتری توسط شناسه
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [DisplayName("واکشی مشتری توسط شناسه")]
        public async Task<OkApiResult<GetCustomerDetailsDto>> GetById([FromQuery] Guid customerId)
        {
            return OkApiResult<GetCustomerDetailsDto>.Ok(await _customerService.GetByIdAsync(customerId));
        }

        /// <summary>
        /// ایجاد مشتری
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [DisplayName("ایجاد مشتری")]
        public async Task<OkApiResult<Guid>> Create([FromBody] CreateCustomerDto createCustomerDto)
        {
            return OkApiResult<Guid>.Ok(await _customerService.CreateAsync(createCustomerDto));
        }

        /// <summary>
        /// ویرایش مشتری
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [DisplayName("ویرایش مشتری")]
        public async Task<OkApiResult<bool>> Update([FromBody] UpdateCustomerDto updateCustomerDto)
        {
            return OkApiResult<bool>.Ok(await _customerService.UpdateAsync(updateCustomerDto));
        }

        /// <summary>
        /// حذف مشتری
        /// </summary>
        [HttpPost]
        [DisplayName("حذف مشتری")]
        public async Task<OkApiResult<bool>> Delete([FromQuery] Guid customerId)
        {
            return OkApiResult<bool>.Ok(await _customerService.DeleteAsync(customerId));
        }
    }
}
