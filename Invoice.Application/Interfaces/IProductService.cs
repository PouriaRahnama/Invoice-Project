using Invoice.Application.Dtos.ProductDtos;

namespace Invoice.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<GetAllProductsDto>> GetAllAsync();
        Task<GetProductDetailsDto> GetByIdAsync(Guid productId);
        Task<Guid> CreateAsync(CreateProductDto createProductDto);
        Task<bool> UpdateAsync(UpdateProductDto updateProductDto);
        Task<bool> DeleteAsync(Guid productId);
    }
}
