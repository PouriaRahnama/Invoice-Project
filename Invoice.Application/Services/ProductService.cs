namespace Invoice.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this._productRepository = productRepository;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
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
                throw new Exception("محصول مورد نظر پیدا نشد.");

            await _productRepository.DeleteAsync(productId);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
       
        public async Task<IEnumerable<GetAllProductsDto>> GetAllAsync()
        {
            IQueryable<Product> products = _productRepository.EntitiesAsNoTracking;

            var productsProjected = await products
                    .ProjectTo<GetAllProductsDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

            if (productsProjected == null || productsProjected.Count == 0)
                return new List<GetAllProductsDto>();

            return productsProjected;
        }
        
        public async Task<GetProductDetailsDto> GetByIdAsync(Guid productId)
        {
            var product = await _productRepository
                .EntitiesAsNoTracking.Where(p => p.Id == productId)
                .ProjectTo<GetProductDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (product == null) return new GetProductDetailsDto();

            return product;
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
