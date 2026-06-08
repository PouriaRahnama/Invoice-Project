namespace Invoice.Application.Dtos.UserRefreshTokenDto
{
    public class GenerateNewUserRefreshTokenDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
