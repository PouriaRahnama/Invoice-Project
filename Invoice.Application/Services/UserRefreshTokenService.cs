namespace Invoice.Application.Services
{
    public class UserRefreshTokenService : IUserRefreshTokenService
    {
        private readonly JwtTokenUtility _jwtTokenUtility;
        private readonly JwtSettings _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserRefreshTokenService(IUserRefreshTokenRepository userRefreshTokenRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptions<JwtSettings> jwtSettings,
            JwtTokenUtility jwtTokenUtility,
            IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            this._userRefreshTokenRepository = userRefreshTokenRepository;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._jwtSettings = jwtSettings.Value;
            this._jwtTokenUtility = jwtTokenUtility;
            this._userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Guid> CreateAsync(CreateUserRefreshTokenDto createUserRefreshTokenDto)
        {
            var userRefreshToken = _mapper.Map<UserRefreshToken>(createUserRefreshTokenDto);
            userRefreshToken.ExpireDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenDurationInDays);
            userRefreshToken.DeviceName = Extensions.GetDeviceName(_httpContextAccessor.HttpContext!);

            await _userRefreshTokenRepository.CreateAsync(userRefreshToken);
            await _unitOfWork.SaveChangesAsync();

            return userRefreshToken.Id;
        }

        public async Task<TokenInfoDto> GenerateNewUserTokenAsync(string refreshToken)
        {
            var storedToken = await _userRefreshTokenRepository.Entities
                .SingleOrDefaultAsync(rf => rf.RefreshToken == Extensions.ComputeSha256(refreshToken));

            if (storedToken == null)
                throw new UnauthorizedException("رفرش توکن نامعتبر است");

            if (storedToken.IsRevoked)
                throw new UnauthorizedException("رفرش توکن باطل شده است");

            if (storedToken.ExpireDate < DateTime.UtcNow)
                throw new UnauthorizedException("رفرش توکن منقضی شده است");

            var user = await _userRepository.GetByIdAsync(storedToken.UserId);

            var newAccessToken = _jwtTokenUtility.GetNewToken(user);
            var newRefreshToken = _jwtTokenUtility.GetNewRefreshToken();

            storedToken.IsRevoked = true;
            storedToken.RevokedDate = DateTime.UtcNow;

            var expireDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenDurationInDays);

            var newToken = new UserRefreshToken
            {
                UserId = user.Id,
                RefreshToken = Extensions.ComputeSha256(newRefreshToken),
                ExpireDate = expireDate,
                DeviceName = Extensions.GetDeviceName(_httpContextAccessor.HttpContext!),
                IsRevoked = false
            };

            await _userRefreshTokenRepository.CreateAsync(newToken);
            await _unitOfWork.SaveChangesAsync();

            return new TokenInfoDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                AccessTokenExpires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes)
            };
        }

        public async Task<bool> RevokeAsync(string refreshToken)
        {
            var storedToken = await _userRefreshTokenRepository.Entities
                 .SingleOrDefaultAsync(rf => rf.RefreshToken == Extensions.ComputeSha256(refreshToken));

            if (storedToken == null)
                throw new NotFoundException("رفرش توکن یافت نشد ");

            storedToken.IsRevoked = true;
            storedToken.RevokedDate = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();

            return true;    
        }
    }
}
