using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class CourierConfiguration : IEntityTypeConfiguration<Courier>
    {
        public void Configure(EntityTypeBuilder<Courier> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();

            builder
                .HasOne(x => x.User)
                .WithOne(x => x.Courier)
                .HasForeignKey<Courier>(x => x.UserId);

            builder
                .HasOne(x => x.Vehicle)
                .WithOne(x => x.Courier)
                .HasForeignKey<Courier>(x => x.VehicleId);
        }
    }
}
