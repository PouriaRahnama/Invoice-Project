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
        public async Task<OkApiResult<GenerateNewUserRefreshTokenDto>> GenerateNewToken([FromBody] string refreshToken)
        {
            return OkApiResult<GenerateNewUserRefreshTokenDto>
                .Ok(await _userRefreshTokenService.GenerateNewUserRefreshTokenAsync(refreshToken));
        }

        /// <summary>
        /// خروج از سیستم
        /// </summary>
        [HttpPost]
        [DisplayName("خروج از سیستم")]
        public async Task<OkApiResult<bool>> Logout([FromBody] string refreshToken)
        {
            return OkApiResult<bool>.Ok(await _userRefreshTokenService.RevokeAsync(refreshToken));
        }

        /// <summary>
        /// واکشی کاربران سیستم - واکشی کاربر توسط شناسه
        /// </summary>
        [HttpGet]
        [DisplayName("واکشی کاربران سیستم - واکشی کاربر توسط شناسه")]
        public async Task<OkApiResult<IEnumerable<GetAllUserAccountsDto>>> GetAll([FromQuery] Guid? userId)
        {
            var id = userId.HasValue ? userId.Value : Guid.Empty;
            return OkApiResult<IEnumerable<GetAllUserAccountsDto>>.Ok(await _userService.GetAllAsync(id));
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
