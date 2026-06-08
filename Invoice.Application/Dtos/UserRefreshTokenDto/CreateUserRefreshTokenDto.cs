namespace Invoice.Application.Dtos.UserRefreshTokenDto
{
    public class CreateUserRefreshTokenDto
    {
        public required string RefreshToken { get; set; }
        public Guid UserId { get; set; }
    }
}
