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
        public IEnumerable<CategoryDTO> GetCategories()
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

        public CategoryDTO CreateCategory(CategoryDTO category)
        {
            var mapped = _mapper.Map<Category>(category);
            _uow.Categories.Create(mapped);
            return _mapper.Map<CategoryDTO>(mapped);
        }

        public IEnumerable<LotDTO> GetLotsByCategory(int id)
        {
            var mapped = _mapper.Map<IEnumerable<LotDTO>>(_uow.Lots.Find(x => x.CategoryId.Equals(id)));
            return mapped;
        }

        public void Dispose()
        {
            _uow.Dispose();
        }

        public void DeleteCategory(int id)
        {
            _uow.Categories.Delete(id);
            _uow.Save();
        }

        public void EditCategory(CategoryDTO category)
        {
            throw new NotImplementedException();
        }
    }
}
