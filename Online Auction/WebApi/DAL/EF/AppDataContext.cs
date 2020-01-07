using DAL.EF.EntitiesConfigurations;
using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EF
{
    /// <summary>
    /// Database context of the application.
    /// </summary>
    public class AppDataContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public DbSet<Lot> Lots { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }


        public AppDataContext(DbContextOptions<AppDataContext> options)
            :base (options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CategoryConfigurations());

            builder.ApplyConfiguration(new LotConfigurations());
            
            builder.ApplyConfiguration(new RoleConfigurations());

            builder.ApplyConfiguration(new BidConfigurations());
        }
    }
}
