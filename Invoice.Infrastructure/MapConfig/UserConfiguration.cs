using Invoice.Domain.Common;

namespace Invoice.Infrastructure.MapConfig
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Users");
            builder.HasQueryFilter(x => !EF.Property<bool>(x, "IsDeleted"));
            builder.HasIndex(x => x.Username);
            // Relations
            builder.HasMany(e => e.Customers)
                   .WithOne(e => e.User)
                   .HasForeignKey(cu => cu.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Invoices)
                   .WithOne(e => e.User)
                   .HasForeignKey(ul => ul.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
