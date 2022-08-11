using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class GoodToCartConfiguration : IEntityTypeConfiguration<GoodToCart>
    {
        public void Configure(EntityTypeBuilder<GoodToCart> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();

            builder
                .HasOne(x => x.Good)
                .WithOne()
                .HasForeignKey<GoodToCart>(x => x.GoodId);
        }
    }
}
