namespace Invoice.Application.Services
{
    public class UserService : IUserService
    {
        private readonly JwtTokenUtility _jwtTokenUtility;
        private readonly JwtSettings _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRefreshTokenService _userRefreshTokenService;

        public UserService(IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptions<JwtSettings> jwtSettings,
            IHttpContextAccessor httpContextAccessor,
            JwtTokenUtility jwtTokenUtility,
            IUserRefreshTokenService userRefreshTokenService)
        {
            this._userRepository = userRepository;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._jwtSettings = jwtSettings.Value;
            this._httpContextAccessor = httpContextAccessor;
            this._jwtTokenUtility = jwtTokenUtility;
            this._userRefreshTokenService = userRefreshTokenService;
        }
        
        public async Task<TokenInfoDto> LoginUserAsync(LoginUserAccountDto loginUserAccountDto)
        {
            var user = await _userRepository.EntitiesAsNoTracking
                .FirstOrDefaultAsync(u => u.Username == loginUserAccountDto.Username);

            if (user == null) throw new BusinessException("نام کاربری یا رمز عبور اشتباه می باشد.");

            var hashPassowrd = EncryptionUtility.GetSHA256(loginUserAccountDto.Password, user.PasswordSalt);
            if (user.PasswordHash != hashPassowrd) throw new BusinessException("نام کاربری یا رمز عبور اشتباه می باشد.");

            var accessToken = _jwtTokenUtility.GetNewToken(user);
            var refreshToken =  _jwtTokenUtility.GetNewRefreshToken();

            await _userRefreshTokenService.CreateAsync(new CreateUserRefreshTokenDto
            {
                UserId = user.Id,
                RefreshToken = refreshToken
            });

            TokenInfoDto token = new()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes)
            };

            return token;
        }
      
        public async Task<bool> RegisterUserAsync(RegisterUserAccountDto registerUserAccountDto)
        {
            var existingUser = await _userRepository.EntitiesAsNoTracking
                .FirstOrDefaultAsync(u => u.Username == registerUserAccountDto.Username || u.Phone == registerUserAccountDto.Phone);

            if (existingUser != null) throw new BusinessException("کاربر از قبل وجود دارد");

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

            var productsProjected = await users
                .ProjectTo<GetAllUserAccountsDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (productsProjected == null || productsProjected.Count == 0)
                return new List<GetAllUserAccountsDto>();

            return productsProjected;
        }
      
        public async Task<GetUserAccountDetailsDto> GetCurrentUserInformation()
        {
            var userId = _httpContextAccessor.HttpContext.GetUserId();

            if (userId == null || userId == Guid.Empty)
                throw new UnauthorizedException("کاربر احراز هویت نشده است");

            var user = await _userRepository
                .EntitiesAsNoTracking.Where(p => p.Id == userId)
                .ProjectTo<GetUserAccountDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (user == null) throw new NotFoundException("کاربر در سیستم یافت نشد ");

            return user;
        }
    }
}
