using System.Collections.Generic;

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
