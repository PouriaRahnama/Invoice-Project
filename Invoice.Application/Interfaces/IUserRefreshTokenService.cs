using Invoice.Application.Dtos.UserRefreshTokenDto;

namespace Invoice.Application.Interfaces
{
    public interface IUserRefreshTokenService
    {
        Task<Guid> CreateAsync(CreateUserRefreshTokenDto createUserRefreshTokenDto);
        Task<GenerateNewUserRefreshTokenDto> GenerateNewUserRefreshTokenAsync(string refreshToken);
        Task RevokeAsync(string refreshToken);
    }
}
