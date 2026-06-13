namespace Invoice.Application.Common
{
    public class JwtTokenUtility
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenUtility(IOptions<JwtSettings> options)
        {
            this._jwtSettings = options.Value;
        }

        public string GetNewToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username.ToString() ?? ""),
                new Claim(ClaimTypes.MobilePhone,user.Phone.ToString() ?? ""),
            };

            int expireTime = _jwtSettings.DurationInMinutes;
            var _key = _jwtSettings.Key;
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expireTime),
                signingCredentials: signingCredentials);

            string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return accessToken;
        }

        public string GetNewRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

    }
}
