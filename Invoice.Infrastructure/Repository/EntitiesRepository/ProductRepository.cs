using Invoice.Infrastructure.Repository.InterfacesRepository;

namespace Invoice.Infrastructure.Repository.EntitiesRepository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
