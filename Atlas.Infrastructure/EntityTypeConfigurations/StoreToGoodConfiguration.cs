using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class StoreToGoodConfiguration : IEntityTypeConfiguration<StoreToGood>
    {
        public void Configure(EntityTypeBuilder<StoreToGood> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();

            builder
                .HasOne(x => x.Good)
                .WithMany(y => y.StoreToGoods)
                .HasForeignKey(x => x.GoodId);

            builder
                .HasOne(x => x.Store)
                .WithMany(y => y.StoreToGoods)
                .HasForeignKey(x => x.StoreId);
        }
    }
}
