namespace Invoice.Application.Dtos.UserDtos
{
    public class GetAllUserAccountsDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public DateTime? CreatedDateTime { get;  set; }
    }
}
