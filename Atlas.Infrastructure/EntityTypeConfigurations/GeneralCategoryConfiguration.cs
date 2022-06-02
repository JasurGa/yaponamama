using System;
using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class GeneralCategoryConfiguration : IEntityTypeConfiguration<GeneralCategory>
    {
        public void Configure(EntityTypeBuilder<GeneralCategory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
        }
    }
}
