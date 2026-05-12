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


    }
}
