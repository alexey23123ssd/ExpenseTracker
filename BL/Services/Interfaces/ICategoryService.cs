    using BL.Models;

namespace BL.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ServiceDataResponse<Category>> CreateCategoryAsync(Category category);
        Task<ServiceDataResponse<IEnumerable<Category>>> GetCategoriesAsync();
        Task<ServiceDataResponse<Category>> GetCategoryByIdAsync(Guid id);
        Task<ServiceDataResponse<Category>> UpdateCategoryAsync(Category category);
        Task<ServiceResponse> DeleteCategoryAsync(Guid categoryId);
    }
}
    