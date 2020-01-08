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
    /// Contains methods for processing entities in Lots table.
    /// </summary>
    public class LotRepository : IRepository<Lot>
    {
        private AppDataContext context;
        public LotRepository(AppDataContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Method for creating lot.
        /// </summary>
        public void Create(Lot item)
        {
            context.Lots.Add(item);
        }

        /// <summary>
        /// Method for deleting lot.
        /// </summary>
        public void Delete(int id)
        {
            var toDelete = context.Lots.Find(id);
            context.Lots.Remove(toDelete);
        }

        /// <summary>
        /// Method for finding lot by expression.
        /// </summary>
        /// <param name="predicate">Expression for finding entities.</param>
        public IEnumerable<Lot> Find(Func<Lot, bool> predicate)
        {
            return context.Lots.Include(x => x.Bids).Where(predicate);
        }

        /// <summary>
        /// Method for fetching lot by id (primary key).
        /// </summary>
        public Lot GetById(int id)
        {
            var lot = context.Lots.Include(x => x.Bids).Where(x => x.Id == id).FirstOrDefault();
            return lot;
        }

        /// <summary>
        /// Method for fetching all lots from table.
        /// </summary>
        public IEnumerable<Lot> GetAll()
        {
            return context.Lots.Include(x => x.Bids);
        }

        /// <summary>
        /// Method for updating lot.
        /// </summary>
        public void Update(Lot item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
