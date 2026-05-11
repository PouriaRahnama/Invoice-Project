using Invoice.Infrastructure.Repository.InterfacesRepository;

namespace Invoice.Infrastructure.Repository.EntitiesRepository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
