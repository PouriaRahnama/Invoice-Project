namespace Invoice.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(RegisterUserAccountDto registerUserAccountDto);
        Task<TokenInfoDto> LoginUserAsync(LoginUserAccountDto loginUserAccountDto);
        Task<SearchQueryResponse<GetAllUserAccountsDto>> GetAllAsync(FilterUsersDto QueryParams);
        Task<GetUserAccountDetailsDto> GetCurrentUserInformation();
    }
}
