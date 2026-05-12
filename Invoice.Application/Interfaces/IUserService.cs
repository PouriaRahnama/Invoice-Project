using Invoice.Application.Dtos.UserDtos;

namespace Invoice.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(RegisterUserAccountDto registerUserAccountDto);
        Task<string> LoginUserAsync(LoginUserAccountDto loginUserAccountDto); 
    }
}
