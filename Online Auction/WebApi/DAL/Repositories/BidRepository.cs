using DAL.EF;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    /// <summary>
    /// Contains methods for processing entities in Bids table.
    /// </summary>
    public class BidRepository : IRepository<Bid>
    {
        private AppDataContext context;
        public BidRepository(AppDataContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Method for creating bid.
        /// </summary>
        public void Create(Bid item)
        {
            context.Bids.Add(item);
        }

        /// <summary>
        /// Method for deleting bid.
        /// </summary>
        public void Delete(int id)
        {
            var toDelete = context.Bids.Find(id);
            context.Bids.Remove(toDelete);
        }

        /// <summary>
        /// Method for finding bids by expression.
        /// </summary>
        /// <param name="predicate">Expression for finding entities.</param>
        public IEnumerable<Bid> Find(Func<Bid, bool> predicate)
        {
            return context.Bids.Where(predicate);
        }

        /// <summary>
        /// Method for fetching bid by id (primary key).
        /// </summary>
        public Bid GetById(int id)
        {
            return context.Bids.Find(id);
        }

        /// <summary>
        /// Method for fetching all bids from table.
        /// </summary>
        public IEnumerable<Bid> GetAll()
        {
            return context.Bids;
        }

        /// <summary>
        /// Method for updating bid.
        /// </summary>
        public void Update(Bid item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
