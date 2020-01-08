using DAL.EF;
using DAL.Interfaces;
using DAL.Models;
using System.Collections.Generic;

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
