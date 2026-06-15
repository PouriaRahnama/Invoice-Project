using Invoice.Application.Dtos.UserRefreshTokenDto;

namespace Invoice.Web.Controllers
{
    public class UserController : ApiBaseController
    {
        public UserController(ILogger<ApiBaseController> logger) : base(logger)
        {
        }

        /// <summary>
        /// ثبت نام
        /// </summary>
        [HttpPost]
        [DisplayName("ثبت نام")]
        [AllowAnonymous]
        public async Task<OkApiResult<bool>> Register([FromBody] RegisterUserAccountDto registerUserAccountDto)
        {
            return OkApiResult<bool>.Ok(await _userService.RegisterUserAsync(registerUserAccountDto));
        }

        /// <summary>
        /// ورود به سیستم
        /// </summary>
        [HttpPost]
        [DisplayName("ورود به سیستم")]
        [AllowAnonymous]
        public async Task<OkApiResult<TokenInfoDto>> Login([FromBody] LoginUserAccountDto loginUserAccountDto)
        {
            return OkApiResult<TokenInfoDto>.Ok(await _userService.LoginUserAsync(loginUserAccountDto));
        }

        /// <summary>
        /// دریافت توکن جدید با رفرش توکن
        /// </summary>
        [HttpPost]
        [DisplayName("دریافت توکن جدید با رفرش توکن")]
        [AllowAnonymous]
        public async Task<OkApiResult<TokenInfoDto>> GenerateNewToken([FromBody] UserRefreshTokenDto userRefreshTokenDto)
        {
            return OkApiResult<TokenInfoDto>
                .Ok(await _userRefreshTokenService.GenerateNewUserTokenAsync(userRefreshTokenDto.RefreshToken));
        }

        /// <summary>
        /// خروج از سیستم
        /// </summary>
        [HttpPost]
        [DisplayName("خروج از سیستم")]
        public async Task<OkApiResult<bool>> Logout([FromBody] UserRefreshTokenDto userRefreshTokenDto)
        {
            return OkApiResult<bool>.Ok(await _userRefreshTokenService.RevokeAsync(userRefreshTokenDto.RefreshToken));
        }

        /// <summary>
        /// واکشی کاربران سیستم - واکشی کاربر توسط شناسه
        /// </summary>
        [HttpGet]
        [DisplayName("واکشی کاربران سیستم - واکشی کاربر توسط شناسه")]
        public async Task<OkApiResult<SearchQueryResponse<GetAllUserAccountsDto>>> GetAll([FromQuery] FilterUsersDto QueryParams)
        {
            return OkApiResult<SearchQueryResponse<GetAllUserAccountsDto>>.Ok(await _userService.GetAllAsync(QueryParams));
        }

        /// <summary>
        /// واکشی کاربر حاضر در سیستم
        /// </summary>
        [HttpGet]
        [DisplayName("واکشی کاربر حاضر در سیستم")]
        public async Task<OkApiResult<GetUserAccountDetailsDto>> GetCurrentUser()
        {
            return OkApiResult<GetUserAccountDetailsDto>.Ok(await _userService.GetCurrentUserInformation());
        }

    }
}
