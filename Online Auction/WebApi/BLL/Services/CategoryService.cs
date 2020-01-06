using AutoMapper;
using BLL.Services;
using BLL.ModelsDTO;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DAL.Models;

namespace BLL.Services
{
    /// <summary>
    /// Contains methods for managing categories.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private IUnitOfWork _uow { get; set; }

        private IMapper _mapper;

        public CategoryService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        /// <summary>
        /// Method for getting all categories.
        /// </summary>
        /// <returns>Collection of categories DTOs.</returns>
        public IEnumerable<CategoryDTO> GetAllCategories()
        {
            return _mapper.Map<IEnumerable<CategoryDTO>>(_uow.Categories.GetAll());
        }

        /// <summary>
        /// Method for getting category by ID.
        /// </summary>
        /// <param name="id">The category ID.</param>
        /// <returns>Category DTO.</returns>
        public CategoryDTO GetCategory(int id)
        {
            return _mapper.Map<CategoryDTO>(_uow.Categories.Get(id));
        }

        /// <summary>
        /// Method for creating category.
        /// </summary>
        /// <param name="category">Category.</param>
        /// <returns>Created Category DTO.</returns>
        public CategoryDTO CreateCategory(CategoryDTO category)
        {
            var mapped = _mapper.Map<Category>(category);
            _uow.Categories.Create(mapped);
            return _mapper.Map<CategoryDTO>(mapped);
        }

        /// <summary>
        /// Method for getting lots by category ID.
        /// </summary>
        /// <param name="id">Category ID.</param>
        /// <returns>Collection of lots DTOs.</returns>
        public IEnumerable<LotDTO> GetLotsByCategory(int id)
        {
            var mapped = _mapper.Map<IEnumerable<LotDTO>>(_uow.Lots.Find(x => x.CategoryId.Equals(id)));
            return mapped;
        }

        /// <summary>
        /// Method for deleting category by ID.
        /// </summary>
        /// <param name="id">Category ID.</param>
        public void DeleteCategory(int id)
        {
            _uow.Categories.Delete(id);
            _uow.Save();
        }

        /// <summary>
        /// Method for updating category.
        /// </summary>
        /// <param name="category">Category.</param>
        public void EditCategory(CategoryDTO category)
        {
            var mapped = _mapper.Map<Category>(category);
            _uow.Categories.Update(mapped);
        }

        #region IDisposable Support
        private bool _isDisposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _uow.Dispose();
                }

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
