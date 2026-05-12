using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Invoice.Application.Dtos.UserDtos
{
    public class DeleteUserAccountDto
    {
        [Required(ErrorMessage = "شناسه کاربر الزامی است")]
        [DisplayName("شناسه کاربر")]
        public Guid UserId { get; set; }
    }
}
