using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Invoice.Application.Dtos.CustomerDtos
{
    public class DeleteCustomerDto
    {
        [Required(ErrorMessage = "شناسه مشتری الزامی است")]
        [DisplayName("شناسه مشتری")]
        public Guid CustomerId { get; set; }
    }
}
