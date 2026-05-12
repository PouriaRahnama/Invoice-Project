using Invoice.Application.Dtos.UserDtos;

namespace Invoice.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, GetAllUserAccountsDto>();
            CreateMap<RegisterUserAccountDto, User>();
            CreateMap<UpdateUserAccountDto, User>();
        }
    }
}
