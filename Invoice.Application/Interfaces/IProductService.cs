using Invoice.Framework.GenericFilters;

namespace Invoice.Application.Interfaces
{
    public interface IProductService
    {
        Task<SearchQueryResponse<GetAllProductsDto>> GetAllAsync(FilterProductsDto QueryParams);
        Task<SearchQueryResponse<GetProductsDto>> GetProductsAsync(FilterProductsDto QueryParams);
        Task<GetProductDetailsDto> GetByIdAsync(Guid productId);
        Task<Guid> CreateAsync(CreateProductDto createProductDto);
        Task<bool> UpdateAsync(UpdateProductDto updateProductDto);
        Task<bool> DeleteAsync(Guid productId);
    }
}
