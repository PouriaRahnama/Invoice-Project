
namespace Invoice.Application.Dtos.ProductDtos
{
    public class GetProductDetailsDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
