
namespace Invoice.Web.Controllers
{
    public class ProductController : ApiBaseController
    {
        public ProductController(ILogger<ApiBaseController> logger) : base(logger){}

        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        [DisplayName("")]
        public async Task<OkApiResult<IEnumerable<GetAllProductsDto>>> GetAll()
        {
            return OkApiResult<IEnumerable<GetAllProductsDto>>.Ok(await _productService.GetAllAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        [DisplayName("")]
        public async Task<OkApiResult<GetProductDetailsDto>> GetById([FromQuery] Guid id)
        {
            return OkApiResult<GetProductDetailsDto>.Ok(await _productService.GetByIdAsync(id));
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        [DisplayName("")]
        public async Task<OkApiResult<Guid>> Create([FromBody] CreateProductDto createProductDto)
        {
            return OkApiResult<Guid>.Ok(await _productService.CreateAsync(createProductDto));
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        [DisplayName("")]
        public async Task<OkApiResult<bool>> Update([FromBody] UpdateProductDto updateProductDto)
        {
            return OkApiResult<bool>.Ok(await _productService.UpdateAsync(updateProductDto));
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        [DisplayName("")]
        public async Task<OkApiResult<bool>> Delete([FromQuery] Guid id)
        {
            return OkApiResult<bool>.Ok(await _productService.DeleteAsync(id));
        }


    }
}
