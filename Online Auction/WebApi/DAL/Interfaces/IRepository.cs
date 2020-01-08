using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    /// <summary>
    /// Generic repository provide all base needed methods (CRUD)
    /// </summary>
    public interface IRepository<T>
    {
        //IEnumerable<T> GetPaged(PagingOptions options);

        /// <summary>
        /// Method for finding entities by expression
        /// </summary>
        /// <param name="predicate">Expression for finding entities.</param>
        IEnumerable<T> Find(Func<T, Boolean> predicate);

        /// <summary>
        /// Method for fetching all data from table.
        /// </summary>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Method for fetching entity by id (primary key).
        /// </summary>
        T GetById(int id);

        /// <summary>
        /// Method for creating entity.
        /// </summary>
        void Create(T item);

        /// <summary>
        /// Method for updating entity.
        /// </summary>
        void Update(T item);

        /// <summary>
        /// Method for deleting entity.
        /// </summary>
        void Delete(int id);
       
    }
}
