using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    /// <summary>
    /// Interface for user profiles repository.
    /// </summary>
    public interface IUserRepository<T>
    {
        /// <summary>
        /// Method for fetching all users from table.
        /// </summary>
        IEnumerable<T> GetAll();
    }
}
