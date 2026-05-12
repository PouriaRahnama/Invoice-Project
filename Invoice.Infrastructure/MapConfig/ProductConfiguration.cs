namespace Invoice.Infrastructure.MapConfig
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Products");
            builder.HasQueryFilter(x => !EF.Property<bool>(x, "IsDeleted"));
            builder.HasIndex(x => x.Name);
            // Relations
            builder.HasMany(e => e.InvoiceItems)
                   .WithOne(e => e.Product)
                   .HasForeignKey(cu => cu.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
