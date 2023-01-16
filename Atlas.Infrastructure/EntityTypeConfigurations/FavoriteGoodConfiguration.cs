using System;
using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class FavoriteGoodConfiguration : IEntityTypeConfiguration<FavoriteGood>
    {
        public void Configure(EntityTypeBuilder<FavoriteGood> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();

            builder.HasOne(x => x.Good)
                .WithMany(x => x.FavoriteGoods);
        }
    }
}
