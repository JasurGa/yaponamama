using System;
using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class ForgotPasswordCodeConfiguration : IEntityTypeConfiguration<ForgotPasswordCode>
    {
        public void Configure(EntityTypeBuilder<ForgotPasswordCode> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
        }
    }
}
