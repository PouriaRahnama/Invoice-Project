using Invoice.Infrastructure.Repository.InterfacesRepository;

namespace Invoice.Infrastructure.Repository.EntitiesRepository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly IApplicationDbContext _dbContext;
        protected DbSet<TEntity> entities;
        public Repository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            entities = _dbContext.Set<TEntity>();
        }


        public IQueryable<TEntity> Entities => entities.AsQueryable();

        public IQueryable<TEntity> EntitiesAsNoTracking => entities.AsNoTracking().AsQueryable();
        public Task<List<TEntity>> GetAllAsync()
        {
            return entities.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var entity = await entities.FindAsync(id);
            if (entity == null)
                throw new Exception("Id invalid, data not found");
            return entity;
        }


        public async Task CreateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("data is null");

            await entities.AddAsync(entity);
        }

        public async Task CreateRangeAsync(List<TEntity> data)
        {
            if (!data.Any())
                throw new Exception("data invalid");

            await entities.AddRangeAsync(data);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            Delete(entity);
        }

        public  void Delete(TEntity entity)
        {
            entities.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("data is null");

            entities.Update(entity);
        }

    }
}