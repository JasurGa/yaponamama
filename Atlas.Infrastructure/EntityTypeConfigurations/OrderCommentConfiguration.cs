using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class OrderCommentConfiguration : IEntityTypeConfiguration<OrderComment>
    {
        public void Configure(EntityTypeBuilder<OrderComment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();

            builder
                .HasOne(x => x.Order)
                .WithMany(y => y.OrderComments)
                .HasForeignKey(x => x.OrderId);

            builder
                .HasOne(x => x.User)
                .WithOne()
                .HasForeignKey<OrderComment>(x => x.UserId);
        }
    }
}

