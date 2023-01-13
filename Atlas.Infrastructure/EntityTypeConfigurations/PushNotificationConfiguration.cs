using System;
using Atlas.Domain;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class PushNotificationConfiguration : IEntityTypeConfiguration<PushNotification>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PushNotification> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
        }
    }
}

