using System;
using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class VerificationRequestConfiguration : IEntityTypeConfiguration<VerificationRequest>
    {
        public void Configure(EntityTypeBuilder<VerificationRequest> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
        }
    }
}

