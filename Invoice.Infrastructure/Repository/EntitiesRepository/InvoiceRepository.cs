using Invoice.Infrastructure.Repository.InterfacesRepository;

namespace Invoice.Infrastructure.Repository.EntitiesRepository
{
    public class InvoiceRepository : Repository<Invoice.Domain.Entities.Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
