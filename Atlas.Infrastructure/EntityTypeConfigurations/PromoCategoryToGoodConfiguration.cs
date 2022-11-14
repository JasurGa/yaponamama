using System;
using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class PromoCategoryToGoodConfiguration : IEntityTypeConfiguration<PromoCategoryToGood>
    {
        public void Configure(EntityTypeBuilder<PromoCategoryToGood> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
        }
    }
}

