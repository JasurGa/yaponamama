using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class CorrectionConfiguration : IEntityTypeConfiguration<Correction>
    {
        public void Configure(EntityTypeBuilder<Correction> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.StoreToGood)
                .WithMany()
                .HasForeignKey(x => x.StoreToGoodId);
        }
    }
}
