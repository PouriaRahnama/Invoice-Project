
namespace Invoice.Application.Dtos
{
    public class TokenInfoDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpires { get; set; }
    }
}
