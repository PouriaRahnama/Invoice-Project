
namespace Invoice.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        Task<int> SaveChangesAsync();
    }
}
