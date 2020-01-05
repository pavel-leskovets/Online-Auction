using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EF.EntitiesConfigurations
{
    public class RoleConfigurations : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            List<AppRole> roles = new List<AppRole>();

            roles.Add(new AppRole { Id = 1, Name = "Admin", NormalizedName = "Admin" });
            roles.Add(new AppRole { Id = 2, Name = "Customer", NormalizedName = "Customer" });
            roles.Add(new AppRole { Id = 3, Name = "Moderator", NormalizedName = "Moderator" });

            builder.HasData(roles);
        }
    }
}
