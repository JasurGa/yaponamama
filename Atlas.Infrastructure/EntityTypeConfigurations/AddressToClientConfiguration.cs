using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class AddressToClientConfiguration : IEntityTypeConfiguration<AddressToClient>
    {
        public void Configure(EntityTypeBuilder<AddressToClient> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();

            builder
                .HasOne(x => x.Client)
                .WithMany(y => y.AddressToClients)
                .HasForeignKey(x => x.ClientId);
        }
    }
}
