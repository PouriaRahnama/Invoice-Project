namespace Invoice.Infrastructure.MapConfig;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Customers");
        builder.HasQueryFilter(x => !EF.Property<bool>(x, "IsDeleted"));
        // Relations
        builder.HasMany(e => e.Invoices)
               .WithOne(e => e.Customer)
               .HasForeignKey(cu => cu.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.User)
               .WithMany(e => e.Customers)
               .HasForeignKey(ul => ul.UserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}

