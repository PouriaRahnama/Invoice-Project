namespace Invoice.Application.Mapping
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<CreateInvoiceDto, Invoice.Domain.Entities.Invoice>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.InvoiceNumber, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Status.Pending))
                .ForMember(dest => dest.TotalPrice, opt => opt.Ignore())
                .ForMember(dest => dest.Items, opt => opt.Ignore()); ;

            CreateMap<Invoice.Domain.Entities.Invoice, GetInvoiceDetailsDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                    src.Status == Status.Pending ? "در انتظار" :
                    src.Status == Status.Paid ? "پرداخت شده" : " "))
                .ForMember(dest => dest.InvoiceNumber, opt => opt.MapFrom(src => src.InvoiceNumber))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice.ToString("N0")))
                .ForMember(dest => dest.InvoiceId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FullName))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.CreatedDateTime,
                    opt => opt.MapFrom(src => EF.Property<DateTime?>(src, "CreatedDateTime")));

            CreateMap<InvoiceItem, GetInvoiceItemDto>()
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.DiscountPercent, opt => opt.MapFrom(src => src.DiscountPercent.ToString("N0") + "%"))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice.ToString("N0")))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice.ToString("N0")))
                .ForMember(dest => dest.CreatedDateTime,
                    opt => opt.MapFrom(src => EF.Property<DateTime?>(src, "CreatedDateTime"))); 
        }
    }
}
