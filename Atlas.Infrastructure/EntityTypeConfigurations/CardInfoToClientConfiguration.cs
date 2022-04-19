﻿using System;
using Atlas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Atlas.Persistence.EntityTypeConfigurations
{
    public class CardInfoToClientConfiguration : IEntityTypeConfiguration<CardInfoToClient>
    {
        public void Configure(EntityTypeBuilder<CardInfoToClient> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
        }
    }
}
