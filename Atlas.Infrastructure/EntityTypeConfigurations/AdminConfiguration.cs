using System;
using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();

            builder.HasOne(x => x.User).WithOne()
                .HasForeignKey<Admin>(x => x.UserId);

            builder.HasOne(x => x.OfficialRole).WithOne()
                .HasForeignKey<Admin>(x => x.OfficialRoleId);
        }
    }
}
