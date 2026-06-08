
namespace Invoice.Application.Dtos.ProductDtos
{
    public class CreateProductDto
    {
        public required string Name { get; set; }
        public required int Price { get; set; }
        public required int Quantity { get; set; }
        public IFormFile? Image { get; set; }

    }
}
