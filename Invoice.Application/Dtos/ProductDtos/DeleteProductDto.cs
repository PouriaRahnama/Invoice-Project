
namespace Invoice.Application.Dtos.ProductDtos
{
    public class DeleteProductDto
    {
        [Required(ErrorMessage = "شناسه محصول الزامی است.")]
        public Guid ProductId { get; set; }
    }
}
