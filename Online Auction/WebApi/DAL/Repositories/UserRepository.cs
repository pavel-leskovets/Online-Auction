using DAL.EF;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    /// <summary>
    /// Contains methods for processing entities in Users table.
    /// </summary>
    public class UserRepository : IUserRepository<AppUser>
    {
        private AppDataContext context;
        public UserRepository(AppDataContext context)
        {
            this.context = context;
            
        }

        /// <summary>
        /// Method for fetching all user profiles from table.
        /// </summary>
        public IEnumerable<AppUser> GetAll()
        {
            return context.Users;
            
        }
    }
}
