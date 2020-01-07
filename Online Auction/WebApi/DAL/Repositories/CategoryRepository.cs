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
    /// Contains methods for processing entities in Categories table.
    /// </summary>
    public class CategoryRepository : IRepository<Category>
    {
        private AppDataContext context;
        public CategoryRepository(AppDataContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Method for creating category.
        /// </summary>
        public void Create(Category item)
        {
            context.Categories.Add(item);

        }

        /// <summary>
        /// Method for deleting category.
        /// </summary>
        public void Delete(int id)
        {
            Category category = context.Categories.Find(id);
            if (category != null)
            {
                context.Remove(category);
            }
        }

        /// <summary>
        /// Method for finding category by expression.
        /// </summary>
        /// <param name="predicate">Expression for finding entities.</param>
        public IEnumerable<Category> Find(Func<Category, bool> predicate)
        {
            return context.Categories.Where(predicate);
        }

        /// <summary>
        /// Method for fetching category by id (primary key).
        /// </summary>
        public Category GetById(int id)
        {
            return context.Categories.Find(id);
        }

        /// <summary>
        /// Method for fetching all categories from table.
        /// </summary>
        public IEnumerable<Category> GetAll()
        {
            return context.Categories.Include(x => x.Lots);
        }

        /// <summary>
        /// Method for updating category.
        /// </summary>
        public void Update(Category item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
