using System;
using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Persistence.EntityTypeConfigurations
{
	public class PhotoToGoodConfiguration : IEntityTypeConfiguration<PhotoToGood>
	{
        public void Configure(EntityTypeBuilder<PhotoToGood> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();

            builder.HasOne(x => x.Good)
                .WithMany(y => y.PhotoToGoods)
                .HasForeignKey(x => x.GoodId);
        }
    }
}

