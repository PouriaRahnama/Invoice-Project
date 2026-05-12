using AutoMapper;
using Invoice.Application.Dtos.ProductDtos;

namespace Invoice.Application.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, GetAllProductsDto>();
            CreateMap<Product, GetProductDetailsDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
        }
    }
}
