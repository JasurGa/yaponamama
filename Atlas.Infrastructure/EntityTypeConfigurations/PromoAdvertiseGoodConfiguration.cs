using System;
using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class PromoAdvertiseGoodConfiguration : IEntityTypeConfiguration<PromoAdvertiseGood>
    {
        public void Configure(EntityTypeBuilder<PromoAdvertiseGood> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
            builder.HasOne(pag => pag.PromoAdvertisePage)
                .WithMany(pap => pap.PromoAdvertiseGoods);
        }
    }
}

