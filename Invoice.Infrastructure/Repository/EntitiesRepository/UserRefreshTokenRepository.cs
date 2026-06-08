using Invoice.Infrastructure.Repository.InterfacesRepository;

namespace Invoice.Infrastructure.Repository.EntitiesRepository
{
    public class UserRefreshTokenRepository : Repository<UserRefreshToken>, IUserRefreshTokenRepository
    {
        public UserRefreshTokenRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
