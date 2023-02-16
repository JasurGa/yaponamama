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
    public class PromoToGoodConfiguration : IEntityTypeConfiguration<PromoToGood>
    {
        public void Configure(EntityTypeBuilder<PromoToGood> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id)
                .IsUnique();

            builder.HasOne(x => x.Good)
                .WithMany(x => x.PromoToGoods)
                .HasForeignKey(x => x.GoodId);
        }
    }
}
