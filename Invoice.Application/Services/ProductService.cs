using Invoice.Application.Dtos.ProductDtos;

namespace Invoice.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductService(IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            this._productRepository = productRepository;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<Guid> CreateAsync(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);

            await _productRepository.CreateAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return product.Id;
        }

        public async Task<bool> DeleteAsync(Guid productId)
        {
            var existingProduct = await _productRepository.GetByIdAsync(productId);

            if (existingProduct == null)
                throw new Exception("");

            await _productRepository.DeleteAsync(productId);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<GetAllProductsDto>> GetAllAsync()
        {
            IQueryable<Product> productQuery = _productRepository.EntitiesAsNoTracking;
            var productsList = await productQuery.ToListAsync();
            return _mapper.Map<IEnumerable<GetAllProductsDto>>(productsList);
        }

        public async Task<GetProductDetailsDto> GetByIdAsync(Guid productId)
        {
            var product = await _productRepository.EntitiesAsNoTracking.FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null) throw new Exception("");

            return _mapper.Map<GetProductDetailsDto>(product);
        }

        public async Task<bool> UpdateAsync(UpdateProductDto updateProductDto)
        {
            var existingProduct = await _productRepository.GetByIdAsync(updateProductDto.ProductId);

            if (existingProduct == null) throw new Exception("محصول مورد نظر موجو نمی باشد");

            _mapper.Map(updateProductDto, existingProduct);

            _productRepository.Update(existingProduct);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
