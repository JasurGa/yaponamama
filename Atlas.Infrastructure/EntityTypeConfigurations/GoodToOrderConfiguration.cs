using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class GoodToOrderConfiguration : IEntityTypeConfiguration<GoodToOrder>
    {
        public void Configure(EntityTypeBuilder<GoodToOrder> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();

            builder
                .HasOne(x => x.Order)
                .WithMany(y => y.GoodToOrders)
                .HasForeignKey(x => x.OrderId);

            builder
                .HasOne(x => x.Good)
                .WithMany(y => y.GoodToOrders)
                .HasForeignKey(x => x.GoodId);
        }
    }
}
