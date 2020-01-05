using DAL.EF;

using DAL.Interfaces;
using DAL.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    /// <summary>
    /// Contains properties with repositories, grant access to repositories and can save changes in DB as a single transaction.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private AppDataContext _context;
        private IRepository<Lot> lotRepository;
        private IRepository<Bid> bidRepository;
        private IRepository<Category> categoryRepository;
        private IUserRepository<AppUser> userRepository;
       
        public UnitOfWork(AppDataContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            UserManager = userManager;
        }

        public UserManager<AppUser> UserManager { get; }


        public IRepository<Category> Categories
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new CategoryRepository(_context);
                return categoryRepository;
            }
        }

        public IRepository<Lot> Lots
        {
            get
            {
                if (lotRepository == null)
                    lotRepository = new LotRepository(_context);
                return lotRepository;
            }
        }
        public IRepository<Bid> Bids
        {
            get
            {
                if (bidRepository == null)
                    bidRepository = new BidRepository(_context);
                return bidRepository;
            }
        }

        public IUserRepository<AppUser> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(_context);
                return userRepository;
            }
        }
        
        /// <summary>
        /// Method for saving db changes.
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }

    #region Dispose 
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        
    #endregion

    }
}
