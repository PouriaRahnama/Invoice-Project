namespace Invoice.Application.Dtos.UserDtos
{
    public class TokenInfoDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpires { get; set; }
    }
}
