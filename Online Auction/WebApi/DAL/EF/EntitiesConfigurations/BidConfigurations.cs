using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EF.EntitiesConfigurations
{
    public class BidConfigurations : IEntityTypeConfiguration<Bid>
    {
        public void Configure(EntityTypeBuilder<Bid> builder)
        {
            builder.ToTable("Bid");

            builder.Property(p => p.BidPrice).IsRequired(true);

            builder.Property(p => p.BidDate).IsRequired(true);

            builder.Property(p => p.LotId).IsRequired(true);




        }
    }
}
