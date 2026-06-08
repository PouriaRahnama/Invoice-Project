namespace Invoice.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, GetAllUserAccountsDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedDateTime,
                     opt => opt.MapFrom(src => EF.Property<DateTime?>(src, "CreatedDateTime"))); ;

            CreateMap<User, GetUserAccountDetailsDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedDateTime,
                     opt => opt.MapFrom(src => EF.Property<DateTime?>(src, "CreatedDateTime"))); ;

            CreateMap<RegisterUserAccountDto, User>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore()) 
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        }
    }
}
