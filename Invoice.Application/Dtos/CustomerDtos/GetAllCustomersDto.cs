namespace Invoice.Application.Dtos.CustomerDtos
{
    //For Get All Customers
    public class GetAllCustomersDto
    {
        public Guid CustomerId { get; set; }
        public Guid UserId { get; set; }
        public required string FullName { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }

    }
}
