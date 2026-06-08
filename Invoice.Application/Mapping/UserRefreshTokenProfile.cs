namespace Invoice.Application.Mapping
{
    public class UserRefreshTokenProfile : Profile
    {
        public UserRefreshTokenProfile()
        {
            CreateMap<CreateUserRefreshTokenDto, UserRefreshToken>()
               .ForMember(d => d.RefreshToken,
                     opt => opt.MapFrom(src => Extensions.ComputeSha256(src.RefreshToken)))
               .ForMember(d => d.ExpireDate,
                   opt => opt.Ignore())
               .ForMember(d => d.IsRevoked,
                   opt => opt.MapFrom(_ => false))
               .ForMember(d => d.UserId,
                   opt => opt.MapFrom(src => src.UserId));
        }
    }
}
