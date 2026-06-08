using Invoice.Application.Dtos.UserRefreshTokenDto;

namespace Invoice.Application.Interfaces
{
    public interface IUserRefreshTokenService
    {
        Task<Guid> CreateAsync(CreateUserRefreshTokenDto createUserRefreshTokenDto);
        Task<TokenInfoDto> GenerateNewUserTokenAsync(string refreshToken);
        Task<bool> RevokeAsync(string refreshToken);
    }
}
