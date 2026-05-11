using Invoice.Infrastructure.Repository.InterfacesRepository;

namespace Invoice.Infrastructure.Repository.EntitiesRepository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
