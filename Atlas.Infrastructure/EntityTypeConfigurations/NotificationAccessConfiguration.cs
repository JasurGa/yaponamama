using System;
using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class NotificationAccessConfiguration : IEntityTypeConfiguration<NotificationAccess>
    {
        public void Configure(EntityTypeBuilder<NotificationAccess> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();

            builder.HasOne(x => x.Notification)
                .WithMany(y => y.NotificationAccesses)
                .HasForeignKey(x => x.NotificationId);
        }
    }
}
