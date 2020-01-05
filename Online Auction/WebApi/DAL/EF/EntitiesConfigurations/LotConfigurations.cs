using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

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

            //builder.Property(p => p.ImageUrl).IsRequired(true);

            builder.Property(p => p.UserId).IsRequired(true);

            builder.Property(p => p.UserId).IsRequired(true);

            List<Lot> lots = new List<Lot>();

            for (int i = 1; i < 11; i++)
            {
                lots.Add(new Lot
                {
                    Id = i,
                    Name = "Lot" + i,
                    UserId = i,
                    InitialPrice = 100,
                    BeginDate = DateTime.Now,
                    CategoryId = i,
                    EndDate = DateTime.Now.AddDays(1),
                    CurrentPrice = 100,
                    Description = "Description" + i
                });
            }

            for (int i = 11, j = 1; i < 21; i++)
            {
                lots.Add(new Lot
                {
                    Id = i,
                    Name = "Lot" + i,
                    UserId = i,
                    InitialPrice = 100,
                    BeginDate = DateTime.Now,
                    CategoryId = j++,
                    EndDate = DateTime.Now.AddDays(1),
                    CurrentPrice = 100,
                    Description = "Description" + i
                });
            }

            builder.HasData(lots);

        }
    }
}
