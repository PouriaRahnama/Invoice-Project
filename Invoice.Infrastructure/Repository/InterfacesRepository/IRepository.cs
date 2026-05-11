namespace Invoice.Infrastructure.Repository.InterfacesRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Entities { get; }
        IQueryable<TEntity> EntitiesAsNoTracking { get; }
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Guid id);
        Task CreateAsync(TEntity entity);
        Task CreateRangeAsync(List<TEntity> entity);
        void Update(TEntity entity);
        Task DeleteAsync(Guid id);
        void Delete(TEntity entity);
    }
 
}