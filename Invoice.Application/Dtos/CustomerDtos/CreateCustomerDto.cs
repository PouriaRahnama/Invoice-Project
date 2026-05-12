using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Invoice.Application.Dtos.CustomerDtos
{
    public class CreateCustomerDto
    {

        //public Guid UserId { get; set; }  از کانتکست کاربر فعلی گرفته میشه

        [Required(ErrorMessage = "نام و نام خانوادگی الزامی است")]
        [DisplayName("نام و نام خانوادگی")]
        public required string FullName { get; set; }

        [DisplayName("شماره تلفن")]
        public string? Phone { get; set; }

        [DisplayName("آدرس")]
        public string? Address { get; set; }
    }
}
