using System;
using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class PushNotificationReadConfiguration : IEntityTypeConfiguration<PushNotificationRead>
    {
        public void Configure(EntityTypeBuilder<PushNotificationRead> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
        }
    }
}

