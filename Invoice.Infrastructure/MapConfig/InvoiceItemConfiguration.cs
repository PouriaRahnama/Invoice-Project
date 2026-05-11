namespace Invoice.Infrastructure.MapConfig
{
    internal class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("InvoiceItems");
            //builder.HasQueryFilter(x => !EF.Property<bool>(x, "IsDeleted"));
            // Relations
            builder.HasOne(e => e.Invoice)
                   .WithMany(e => e.Items)
                   .HasForeignKey(cu => cu.InvoiceId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Product)
                   .WithMany(e => e.InvoiceItems)
                   .HasForeignKey(ul => ul.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
