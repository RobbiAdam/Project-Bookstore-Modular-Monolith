using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Basket.Data.Configurations;
public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
{
    public void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.Username)
               .IsUnique();

        builder.Property(e => e.Username)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasMany(s => s.Items)
           .WithOne()
           .HasForeignKey(si => si.ShoppingCartId);
    }
}
