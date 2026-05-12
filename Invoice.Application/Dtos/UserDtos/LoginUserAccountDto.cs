using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Invoice.Application.Dtos.UserDtos
{
    public class LoginUserAccountDto
    {
        [Required(ErrorMessage = "نام کاربری الزامی است")]
        [DisplayName("نام کاربری")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "رمز عبور الزامی است")]
        [DisplayName("رمز عبور")]
        public required string Password { get; set; }
    }

}
