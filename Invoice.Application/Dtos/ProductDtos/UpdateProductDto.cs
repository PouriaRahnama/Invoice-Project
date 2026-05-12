
namespace Invoice.Application.Dtos.ProductDtos
{
    public class UpdateProductDto : CreateProductDto
    {
        public Guid ProductId { get; set; }
    }
}
