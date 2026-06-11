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

            if (createProductDto.Image != null)
                product.ImagePath = await Extensions
                    .SaveImageAndGenerateName(createProductDto.Image, FilePaths.ProductImagePathSave);

            await _productRepository.CreateAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return product.Id;
        }

        public async Task<bool> DeleteAsync(Guid productId)
        {
            var existingProduct = await _productRepository.GetByIdAsync(productId);

            if (existingProduct == null)
                throw new NotFoundException("محصول مورد نظر یافت نشد");

            if (!string.IsNullOrEmpty(existingProduct.ImagePath))
                Extensions.DeleteFile(existingProduct.ImagePath, FilePaths.ProductImagePathSave);

            await _productRepository.DeleteAsync(productId);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<SearchQueryResponse<GetAllProductsDto>> GetAllAsync(FilterProductsDto QueryParams)
        {
            var mapper = new ProductGridifyMapper();

            var query = _productRepository.EntitiesAsNoTracking
                    .ProjectTo<GetAllProductsDto>(_mapper.ConfigurationProvider)
                    .OrderByDescending(x => EF.Property<DateTime>(x, "CreatedDateTime"))
                    .AsQueryable();

            var qp = await query.GridifyQueryableAsync(QueryParams, mapper);

            var pq = new Paging<GetAllProductsDto>(qp.Count, qp.Query);
            return new SearchQueryResponse<GetAllProductsDto>(QueryParams, pq);
        }

        public async Task<GetProductDetailsDto> GetByIdAsync(Guid productId)
        {
            var product = await _productRepository
                .EntitiesAsNoTracking.Where(p => p.Id == productId)
                .ProjectTo<GetProductDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (product == null) throw new NotFoundException("محصول مورد نظر یافت نشد");

            return product;
        }

        public async Task<bool> UpdateAsync(UpdateProductDto updateProductDto)
        {
            var existingProduct = await _productRepository.GetByIdAsync(updateProductDto.ProductId);
            if (existingProduct == null) throw new NotFoundException("محصول مورد نظر یافت نشد");

            _mapper.Map(updateProductDto, existingProduct);

            if (updateProductDto.Image != null)
            {
                if (!string.IsNullOrEmpty(existingProduct.ImagePath))
                    Extensions.DeleteFile(FilePaths.ProductImagePathSave, existingProduct.ImagePath);

                existingProduct.ImagePath = await Extensions
                  .SaveImageAndGenerateName(updateProductDto.Image, FilePaths.ProductImagePathSave);
            }

            _productRepository.Update(existingProduct);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
