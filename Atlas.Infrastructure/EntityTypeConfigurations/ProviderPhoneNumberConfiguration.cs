using System;
using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class ProviderPhoneNumberConfiguration : IEntityTypeConfiguration<ProviderPhoneNumber>
    {
        public void Configure(EntityTypeBuilder<ProviderPhoneNumber> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
        }
    }
}
