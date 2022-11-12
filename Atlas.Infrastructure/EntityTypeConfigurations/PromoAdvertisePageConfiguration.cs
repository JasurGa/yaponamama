using System;
using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class PromoAdvertisePageConfiguration : IEntityTypeConfiguration<PromoAdvertisePage>
    {
        public void Configure(EntityTypeBuilder<PromoAdvertisePage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
            builder.HasOne(pap => pap.PromoAdvertise)
                .WithMany(pa => pa.PromoAdvertisePages);
            builder.HasMany(pap => pap.PromoAdvertiseGoods)
                .WithOne(pag => pag.PromoAdvertisePage);
        }
    }
}

