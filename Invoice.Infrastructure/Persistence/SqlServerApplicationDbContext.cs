using Invoice.Infrastructure.MapConfig;

namespace Invoice.Infrastructure.Persistence;

public class SqlServerApplicationDbContext : DbContext, IApplicationDbContext
{
    public SqlServerApplicationDbContext(DbContextOptions<SqlServerApplicationDbContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Invoice");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        modelBuilder.OnCreated();
        modelBuilder.OnModified();
        modelBuilder.OnDeleted();
    }

    // Implementation DbSet
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Domain.Entities.Invoice> Invoices { get; set; }
    public DbSet<InvoiceItem> invoiceItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }


}
