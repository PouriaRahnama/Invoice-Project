namespace Invoice.Infrastructure.Persistence;

public interface IApplicationDbContext
{
    #region Structure

    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    int SaveChanges();

    EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
        where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    #endregion
    DbSet<Customer> Customers { get; set; }
    DbSet<Invoice.Domain.Entities.Invoice> Invoices { get; set; }
    DbSet<InvoiceItem> invoiceItems { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<User> Users { get; set; }
}
