using Atlas.Application.Interfaces;
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
    public class ReceiptTypeConfiguration : IEntityTypeConfiguration<Receipt>
    {
        public void Configure(EntityTypeBuilder<Receipt> builder)
        {
            builder.HasKey(x => x.ReceiptId);
            builder.HasIndex(x => x.ReceiptId).IsUnique();
        }
    }
}
