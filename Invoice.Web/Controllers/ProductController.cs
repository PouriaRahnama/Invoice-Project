namespace Invoice.Web.Controllers
{
    public class ProductController : ApiBaseController
    {
        public ProductController(ILogger<ApiBaseController> logger) : base(logger){}

        /// <summary>
        /// واکشی تمام محصولات
        /// </summary>
        [HttpGet]
        [DisplayName("واکشی تمام محصولات")]
        [AllowAnonymous]
        public async Task<OkApiResult<IEnumerable<GetAllProductsDto>>> GetAll()
        {
            return OkApiResult<IEnumerable<GetAllProductsDto>>.Ok(await _productService.GetAllAsync());
        }

        /// <summary>
        /// واکشی محصول توسط شناسه
        /// </summary>
        [HttpGet]
        [DisplayName("واکشی محصول توسط شناسه")]
        [AllowAnonymous]
        public async Task<OkApiResult<GetProductDetailsDto>> GetById([FromQuery] Guid id)
        {
            return OkApiResult<GetProductDetailsDto>.Ok(await _productService.GetByIdAsync(id));
        }

        /// <summary>
        /// ایجاد محصول
        /// </summary>
        [HttpPost]
        [DisplayName("ایجاد محصول")]
        [AllowAnonymous]
        public async Task<OkApiResult<Guid>> Create([FromForm] CreateProductDto createProductDto)
        {
            return OkApiResult<Guid>.Ok(await _productService.CreateAsync(createProductDto));
        }

        /// <summary>
        /// ویرایش محصول
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [DisplayName("ویرایش محصول")]
        public async Task<OkApiResult<bool>> Update([FromForm] UpdateProductDto updateProductDto)
        {
            return OkApiResult<bool>.Ok(await _productService.UpdateAsync(updateProductDto));
        }

        /// <summary>
        /// حذف محصول
        /// </summary>
        [HttpPost]
        [DisplayName("حذف محصول")]
        [AllowAnonymous]
        public async Task<OkApiResult<bool>> Delete([FromQuery] Guid id)
        {
            return OkApiResult<bool>.Ok(await _productService.DeleteAsync(id));
        }


    }
}
