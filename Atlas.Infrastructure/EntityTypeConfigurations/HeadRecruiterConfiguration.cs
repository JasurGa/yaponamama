using System;
using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class HeadRecruiterConfiguration : IEntityTypeConfiguration<HeadRecruiter>
    {
        public void Configure(EntityTypeBuilder<HeadRecruiter> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
        }
    }
}
