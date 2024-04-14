using BL.Models;

namespace BL.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ServiceDataResponse<Guid>> CreateCategory(Category category);
        Task<ServiceDataResponse<IEnumerable<Category>>> GetCategories();
        Task<ServiceDataResponse<Category>> GetCategoryById(Guid id);
        Task<ServiceDataResponse<Category>> UpdateCategory(Category category);
        Task<ServiceResponse> DeleteCategory(Guid categoryId);
    }
}
    