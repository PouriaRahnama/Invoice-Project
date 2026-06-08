namespace Invoice.Infrastructure.MapConfig
{
    public class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("UserRefreshTokens");
            builder.HasQueryFilter(x => !EF.Property<bool>(x, "IsDeleted"));

            builder.Property(x => x.RefreshToken)
                .IsRequired();

            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.RefreshToken).IsUnique();

            // Relations
            builder.HasOne(e => e.User)
                   .WithMany(e => e.UserRefreshTokens)
                   .HasForeignKey(cr => cr.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
