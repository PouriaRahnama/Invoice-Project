using Invoice.Application.Dtos.CustomerDtos;
using Microsoft.AspNetCore.Authorization;

namespace Invoice.Web.Controllers
{
    public class CustomerController : ApiBaseController
    {
        public CustomerController(ILogger<ApiBaseController> logger) : base(logger)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        [DisplayName("")]
        public async Task<OkApiResult<IEnumerable<GetAllCustomersDto>>> GetAll([FromQuery] Guid? userId)
        {
            var id = userId.HasValue ? userId.Value : Guid.Empty;
            return OkApiResult<IEnumerable<GetAllCustomersDto>>.Ok(await _customerService.GetAllAsync(id));
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        [DisplayName("")]
        public async Task<OkApiResult<GetCustomerDetailsDto>> GetById([FromQuery] Guid customerId)
        {
            return OkApiResult<GetCustomerDetailsDto>.Ok(await _customerService.GetByIdAsync(customerId));
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        [DisplayName("")]
        public async Task<OkApiResult<Guid>> Create([FromBody] CreateCustomerDto createCustomerDto)
        {
            return OkApiResult<Guid>.Ok(await _customerService.CreateAsync(createCustomerDto));
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        [DisplayName("")]
        public async Task<OkApiResult<bool>> Update([FromBody] UpdateCustomerDto updateCustomerDto)
        {
            return OkApiResult<bool>.Ok(await _customerService.UpdateAsync(updateCustomerDto));
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        [DisplayName("")]
        public async Task<OkApiResult<bool>> Delete([FromQuery] Guid customerId)
        {
            return OkApiResult<bool>.Ok(await _customerService.DeleteAsync(customerId));
        }



    }
}
