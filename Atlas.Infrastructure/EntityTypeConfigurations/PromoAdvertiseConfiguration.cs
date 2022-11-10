using System;
using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class PromoAdvertiseConfiguration : IEntityTypeConfiguration<PromoAdvertise>
    {
        public void Configure(EntityTypeBuilder<PromoAdvertise> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
            builder.HasMany(pa => pa.PromoAdvertisePages)
                .WithOne(pap => pap.PromoAdvertise);
        }
    }
}

