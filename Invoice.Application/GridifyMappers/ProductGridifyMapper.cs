namespace Invoice.Application.GridifyMappers
{
    public class ProductGridifyMapper : GridifyMapper<GetAllProductsDto>
    {
        public ProductGridifyMapper()
        {
            AddMap("Name", p => p.Name);
            AddMap("ProductId", p => p.ProductId);
            AddMap("Code", p => p.Code);
            AddMap("Price", p => p.Price);
        }
    }
    public class GetProductsGridifyMapper : GridifyMapper<GetProductsDto>
    {
        public GetProductsGridifyMapper()
        {
            AddMap("Name", p => p.Name);
            AddMap("ProductId", p => p.ProductId);
        }
    }
}
