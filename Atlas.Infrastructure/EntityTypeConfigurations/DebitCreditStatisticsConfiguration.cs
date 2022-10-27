using System;
using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class DebitCreditStatisticsConfiguration : IEntityTypeConfiguration<DebitCreditStatistics>
    {
        public void Configure(EntityTypeBuilder<DebitCreditStatistics> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
        }
    }
}

