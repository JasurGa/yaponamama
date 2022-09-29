using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();

            builder
                .HasOne(x => x.VehicleType)
                .WithMany(y => y.Vehicles)
                .HasForeignKey(x => x.VehicleTypeId);

            builder
                .HasOne(x => x.Store)
                .WithMany(y => y.Vehicles)
                .HasForeignKey(x => x.StoreId);
        }
    }
}
