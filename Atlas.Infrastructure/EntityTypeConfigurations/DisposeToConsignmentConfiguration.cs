using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class DisposeToConsignmentConfiguration : IEntityTypeConfiguration<DisposeToConsignment>
    {
        public void Configure(EntityTypeBuilder<DisposeToConsignment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();

            builder
                .HasOne(x => x.Consignment)
                .WithOne(y => y.DisposeToConsignment)
                .HasForeignKey<DisposeToConsignment>(x => x.ConsignmentId);
        }
    }
}
