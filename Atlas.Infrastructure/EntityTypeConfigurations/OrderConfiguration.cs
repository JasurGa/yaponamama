using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();

            builder
                .HasOne(x => x.Courier)
                .WithMany()
                .HasForeignKey(x => x.CourierId);

            builder
                .HasOne(x => x.Client)
                .WithMany()
                .HasForeignKey(x => x.ClientId);

            builder
                .HasOne(x => x.Promo)
                .WithMany()
                .HasForeignKey(x => x.PromoId);

            builder
                .HasOne(x => x.PaymentType)
                .WithMany()
                .HasForeignKey(x => x.PaymentTypeId);

            builder
                .HasOne(x => x.Store)
                .WithMany()
                .HasForeignKey(x => x.StoreId);
        }
    }
}
