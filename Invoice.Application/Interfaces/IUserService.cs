using Invoice.Application.Dtos;

namespace Invoice.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(RegisterUserAccountDto registerUserAccountDto);
        Task<TokenInfoDto> LoginUserAsync(LoginUserAccountDto loginUserAccountDto); 
    }
}
