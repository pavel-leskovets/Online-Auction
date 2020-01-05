using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EF.EntitiesConfigurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.Property(p => p.Name).IsRequired(true);

            List<Category> categories = new List<Category>();

            for (int i = 1; i < 11; i++)
            {
                categories.Add(new Category { Id = i, Name = "category " + i });
            }

            builder.HasData(categories);
        }
    }
}
