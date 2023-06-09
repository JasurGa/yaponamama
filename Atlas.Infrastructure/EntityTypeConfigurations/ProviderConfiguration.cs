﻿using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();

            builder
                .HasMany(x => x.ProviderPhoneNumbers)
                .WithOne(y => y.Provider);

            builder
                .HasMany(x => x.Goods)
                .WithOne(y => y.Provider);
        }
    }
}
