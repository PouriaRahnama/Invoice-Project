namespace Invoice.Application.Dtos.CustomerDtos
{
    public class GetCustomerDetailsDto
    {
        public Guid CustomerId { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime? CreatedDateTime { get; set; }

    }
}
