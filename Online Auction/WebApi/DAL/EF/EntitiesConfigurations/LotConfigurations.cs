using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace DAL.EF.EntitiesConfigurations
{
    public class LotConfigurations : IEntityTypeConfiguration<Lot>
    {
        public void Configure(EntityTypeBuilder<Lot> builder)
        {
            builder.ToTable("Lot");

            builder.Property(p => p.Name).IsRequired(true).HasMaxLength(30);

            builder.Property(p => p.BeginDate).IsRequired(true);

            builder.Property(p => p.UserId).IsRequired(true);

            builder.Property(p => p.CategoryId).IsRequired(true);

            builder.Property(p => p.Description).IsRequired(true).HasMaxLength(200);

            builder.Property(p => p.InitialPrice).IsRequired(true);

            builder.Property(p => p.UserId).IsRequired(true);

            builder.Property(p => p.UserId).IsRequired(true);

        }
    }
}
