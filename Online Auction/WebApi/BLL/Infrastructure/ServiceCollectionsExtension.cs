using AutoMapper;
using BLL.Mapping;
using DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

using DAL.Models;
using DAL.Repositories;
using DAL.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BLL.Infrastructure
{
    public static class ServiceCollectionsExtension
    {
        public static IServiceCollection RegisterBllServices(this IServiceCollection services, string connectionString)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);

            services.AddIdentityCore<AppUser>().AddRoles<AppRole>().AddEntityFrameworkStores<AppDataContext>();

            services.AddDbContext<AppDataContext>(options =>
                options.UseSqlServer(connectionString));

         
            services.AddScoped<IRepository<Category>, CategoryRepository>();
            services.AddScoped<IRepository<Lot>, LotRepository>();
            services.AddScoped<IRepository<Bid>, BidRepository>();
            services.AddScoped<IUserRepository<AppUser>, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ILotService, LotService>();
            services.AddScoped<IBidService, BidService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();


           




            return services;
        }
    }
}
