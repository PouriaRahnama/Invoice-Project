namespace Invoice.Application.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, GetAllCustomersDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone ?? string.Empty))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address ?? string.Empty))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.CreatedDateTime,
                    opt => opt.MapFrom(src => EF.Property<DateTime?>(src, "CreatedDateTime")));


            CreateMap<Customer, GetCustomerDetailsDto>()
                 .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone ?? string.Empty))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address ?? string.Empty))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.CreatedDateTime,
                    opt => opt.MapFrom(src => EF.Property<DateTime?>(src, "CreatedDateTime"))); ;



            CreateMap<CreateCustomerDto, Customer>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone ?? string.Empty))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address ?? string.Empty))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));


            CreateMap<UpdateCustomerDto, Customer>()
                 .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone ?? string.Empty))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address ?? string.Empty))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));
        }
    }
}
