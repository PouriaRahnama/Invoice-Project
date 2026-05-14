using Invoice.Application.Dtos;
using Invoice.Application.Dtos.UserDtos;
using Microsoft.AspNetCore.Authorization;

namespace Invoice.Web.Controllers
{
    public class UserController : ApiBaseController
    {
        public UserController(ILogger<ApiBaseController> logger) : base(logger)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        [DisplayName("")]
        [AllowAnonymous]
        public async Task<OkApiResult<bool>> Register([FromBody] RegisterUserAccountDto registerUserAccountDto)
        {
            return OkApiResult<bool>.Ok(await _userService.RegisterUserAsync(registerUserAccountDto));
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        [DisplayName("")]
        [AllowAnonymous]
        public async Task<OkApiResult<TokenInfoDto>> Login([FromBody] LoginUserAccountDto loginUserAccountDto)
        {
            return OkApiResult<TokenInfoDto>.Ok(await _userService.LoginUserAsync(loginUserAccountDto));
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        [DisplayName("")]
        public async Task<OkApiResult<IEnumerable<GetAllUserAccountsDto>>> GetAll([FromQuery] Guid? userId)
        {
            var id = userId.HasValue ? userId.Value : Guid.Empty;
            return OkApiResult<IEnumerable<GetAllUserAccountsDto>>.Ok(await _userService.GetAllAsync(id));
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        [DisplayName("")]
        public async Task<OkApiResult<GetUserAccountDetailsDto>> GetCurrentUser()
        {
            return OkApiResult<GetUserAccountDetailsDto>.Ok(await _userService.GetCurrentUserInformation());
        }

    }
}
