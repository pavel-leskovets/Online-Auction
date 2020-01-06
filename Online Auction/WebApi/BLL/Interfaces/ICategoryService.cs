using BLL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    /// <summary>
    /// Interface for categories service.
    /// Contains methods for managing categories.
    /// </summary>
    public interface ICategoryService : IDisposable
    {
        /// <summary>
        /// Method for getting all categories.
        /// </summary>
        /// <returns>Collection of categories DTOs.</returns>
        IEnumerable<CategoryDTO> GetAllCategories();

        /// <summary>
        /// Method for getting lots by category ID.
        /// </summary>
        /// <returns>Collection of categories DTOs.</returns>
        /// <param name="categoryId">Category ID</param>
        IEnumerable<LotDTO> GetLotsByCategory(int categoryId);

        /// <summary>
        /// Method for creating lot.
        /// </summary>
        /// <returns>Created category DTO.</returns>
        /// <param name="category">New category</param>
        CategoryDTO CreateCategory(CategoryDTO category);

        /// <summary>
        /// Method for getting category by ID.
        /// </summary>
        /// <returns>Created category DTO.</returns>
        /// <param name="id">Category ID</param>
        CategoryDTO GetCategory(int id);

        /// <summary>
        /// Method for updating category.
        /// </summary>
        /// <returns>Created category DTO.</returns>
        /// <param name="category">Category.</param>
        void EditCategory(CategoryDTO category);

        /// <summary>
        /// Method for deleting category by ID.
        /// </summary>
        /// <returns>Created category DTO.</returns>
        /// <param name="id">Category ID</param>
        void DeleteCategory(int id);
    }
}
