namespace Invoice.Infrastructure.MapConfig;

internal class InvoiceConfiguration : IEntityTypeConfiguration<Invoice.Domain.Entities.Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice.Domain.Entities.Invoice> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Invoice");
        //builder.HasQueryFilter(x => !EF.Property<bool>(x, "IsDeleted"));
        // Relations
        builder.HasMany(e => e.Items)
               .WithOne(e => e.Invoice)
               .HasForeignKey(cu => cu.InvoiceId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Customer)
               .WithMany(e => e.Invoices)
               .HasForeignKey(ul => ul.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(e => e.User)
               .WithMany(e => e.Invoices)
               .HasForeignKey(ul => ul.UserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}

