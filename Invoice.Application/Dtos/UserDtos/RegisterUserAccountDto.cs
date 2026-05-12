using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Invoice.Application.Dtos.UserDtos
{
    public class RegisterUserAccountDto
    {
        [Required(ErrorMessage = "نام کاربری الزامی است")]
        [DisplayName("نام کاربری")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "شماره تلفن الزامی است")]
        [DisplayName("شماره تلفن")]
        public required string Phone { get; set; }

        [Required(ErrorMessage = "رمز عبور الزامی است")]
        [DisplayName("رمز عبور")]
        public required string Password { get; set; }
    }

}
