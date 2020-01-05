using BLL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetCategories();
        IEnumerable<LotDTO> GetLotsByCategory(int categoryId);
        CategoryDTO CreateCategory(CategoryDTO category);
        CategoryDTO GetCategory(int id);
        void EditCategory(CategoryDTO category);
        void DeleteCategory(int id);
        void Dispose();
    }
}
