namespace Invoice.Application.Dtos.UserDtos
{
    public class GetUserAccountDetailsDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
    }
}
