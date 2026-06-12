
namespace Invoice.Application.Dtos.ProductDtos
{
    public class UpdateProductDto : CreateProductDto
    {
        [Required(ErrorMessage = "شناسه محصول الزامی است.")]
        public Guid ProductId { get; set; }
    }
}
