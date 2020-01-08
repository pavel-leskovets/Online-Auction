using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace DAL.Interfaces
{
    /// <summary>
    /// Defines the interface for unit of work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {

        UserManager<AppUser> UserManager { get; }

        /// <summary>
        /// Gets category repository.
        /// </summary>
        IRepository<Category> Categories { get; }

        /// <summary>
        /// Gets lots repository.
        /// </summary>
        IRepository<Lot> Lots { get; }

        /// <summary>
        /// Gets bids repository.
        /// </summary>
        IRepository<Bid> Bids { get; }

        /// <summary>
        /// Gets users repository.
        /// </summary>
        IUserRepository<AppUser> Users { get; }

        /// <summary>
        /// Method for saving db changes.
        /// </summary>
        void Save();
    }
}
