using BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Interfaces
{
    public interface ICategoryService
    {
        ServiceDataResponse<Guid> CreateCategory(Category category);
        ServiceDataResponse<IEnumerable<Category>> GetCategories();
        ServiceDataResponse<Category> GetCategoryById(Guid id);
        ServiceDataResponse<Category> UpdateCategory(Category category);
        ServiceResponse DeleteCategory(Guid categoryId);
    }
}
