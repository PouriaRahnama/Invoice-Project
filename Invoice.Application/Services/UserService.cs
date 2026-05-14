
using Invoice.Application.Dtos.UserDtos;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Application.Services
{
    public class UserService : IUserService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptions<JwtSettings> jwtSettings,
            IHttpContextAccessor httpContextAccessor)
        {
            this._userRepository = userRepository;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._jwtSettings = jwtSettings.Value;
            this._httpContextAccessor = httpContextAccessor;
        }
        
        public async Task<TokenInfoDto> LoginUserAsync(LoginUserAccountDto loginUserAccountDto)
        {
            var user = await _userRepository.EntitiesAsNoTracking
                .FirstOrDefaultAsync(u => u.Username == loginUserAccountDto.Username);

            if (user == null) throw new Exception("نام کاربری یا رمز عبور اشتباه می باشد.");

            var hashPassowrd = EncryptionUtility.GetSHA256(loginUserAccountDto.Password, user.PasswordSalt);
            if (user.PasswordHash != hashPassowrd) throw new Exception("نام کاربری یا رمز عبور اشتباه می باشد.");

            var accessToken = GetNewToken(user);
            TokenInfoDto token = new()
            {
                AccessToken = accessToken,
                RefreshToken = EncryptionUtility.GetNewRefreshToken(),
                AccessTokenExpires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes)
            };

            return token;
        }

        
        public async Task<bool> RegisterUserAsync(RegisterUserAccountDto registerUserAccountDto)
        {
            var existingUser = await _userRepository.EntitiesAsNoTracking
                .FirstOrDefaultAsync(u => u.Username == u.Username || u.Phone == u.Phone);

            if (existingUser != null) throw new Exception("کاربر از قبل وجود دارد");

            string passwordSalt = EncryptionUtility.GetNewSalt();
            string passwordHash = EncryptionUtility.GetSHA256(registerUserAccountDto.Password, passwordSalt);

            var user = _mapper.Map<User>(registerUserAccountDto);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;

            await _userRepository.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        
        public async Task<IEnumerable<GetAllUserAccountsDto>> GetAllAsync(Guid? userId)
        {
            var users = _userRepository.EntitiesAsNoTracking;

            if (userId != Guid.Empty)
                users = users.Where(c => c.Id == userId.Value);

            var usersList = await users.ToListAsync();

            if (usersList == null || usersList.Count() == 0)
                return new List<GetAllUserAccountsDto>();

            return _mapper.Map<IEnumerable<GetAllUserAccountsDto>>(usersList);
        }

        
        public async Task<GetUserAccountDetailsDto> GetCurrentUserInformation()
        {
            var userId = _httpContextAccessor.HttpContext.GetUserId();

            if (userId == null || userId == Guid.Empty)
                throw new Exception("کاربر در سیستم وارد نشده است.");

            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null) throw new Exception("کاربر در سیستم وجود ندارد.");

            return _mapper.Map<GetUserAccountDetailsDto>(user);
        }

        /// <summary>
        /// jwt تولید توکن 
        /// </summary>
        private string GetNewToken(User user)
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
                expires: DateTime.UtcNow.AddMinutes(expireTime),
                signingCredentials: signingCredentials);

            string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return accessToken;
        }


    }
}
