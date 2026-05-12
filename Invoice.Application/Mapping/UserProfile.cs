namespace Invoice.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, GetAllUserAccountsDto>();



            CreateMap<RegisterUserAccountDto, User>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore()) 
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());


            CreateMap<UpdateUserAccountDto, User>();


        }
    }
}
