using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class ConsignmentConfiguration : IEntityTypeConfiguration<Consignment>
    {
        public void Configure(EntityTypeBuilder<Consignment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();

            builder
                .HasOne(x => x.StoreToGood)
                .WithMany(x => x.Consignments)
                .HasForeignKey(x => x.StoreToGoodId);
        }
    }
}
