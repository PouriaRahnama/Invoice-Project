using Azure.Core;
using Invoice.Application.Dtos;

namespace Invoice.Application.Services
{
    public class UserService : IUserService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptions<JwtSettings> jwtSettings)
        {
            this._userRepository = userRepository;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._jwtSettings = jwtSettings.Value;
        }
        //done
        public async Task<TokenInfoDto> LoginUserAsync(LoginUserAccountDto loginUserAccountDto)
        {
            var user = await _userRepository.EntitiesAsNoTracking
                .FirstOrDefaultAsync(u => u.Username == loginUserAccountDto.Username);

            if (user == null) throw new Exception(".");

            var hashPassowrd = EncryptionUtility.GetSHA256(loginUserAccountDto.Password, user.PasswordSalt);
            if (user.PasswordHash != hashPassowrd) throw new Exception();

            var accessToken = GetNewToken(user.Id);
            TokenInfoDto token = new()
            {
                AccessToken = accessToken,
                RefreshToken = EncryptionUtility.GetNewRefreshToken(),
                AccessTokenExpires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes)
            };

            return token;
        }

        //done
        public async Task<bool> RegisterUserAsync(RegisterUserAccountDto registerUserAccountDto)
        {
            var existingUser = await _userRepository.EntitiesAsNoTracking
                .FirstOrDefaultAsync(u => u.Username == u.Username || u.Phone == u.Phone);

            if (existingUser != null) throw new Exception("کاربر از قبل موجوداست");

            string passwordSalt = EncryptionUtility.GetNewSalt();
            string passwordHash = EncryptionUtility.GetSHA256(registerUserAccountDto.Password, passwordSalt);

            var user = _mapper.Map<User>(registerUserAccountDto);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;

            await _userRepository.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        //done
        /// <summary>
        /// jwt تولید توکن 
        /// </summary>
        private string GetNewToken(Guid userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            };

            int expireTime = _jwtSettings.DurationInMinutes;
            var _key = _jwtSettings.Key;
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireTime),
                signingCredentials: signingCredentials);

            string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return accessToken;
        }

    }
}
