using AutoMapper;
using BL.Models;
using BL.Services.Interfaces;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace BL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ExpenseTrackerDbContext _dbContext;
        private readonly IMapper _mapper; 
        public CategoryService(ExpenseTrackerDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext??throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<ServiceDataResponse<Category>> CreateCategoryAsync(Category category)
        {
            if(category == null)
            {
               return  ServiceDataResponse<Category>.Failed("Data cannot be null");
            }

            if(await _dbContext.Categories.AnyAsync(c => c.Id == category.Id))
            {
                return ServiceDataResponse<Category>.Failed("Category with this name already exist");
            }

            var categoryId = Guid.NewGuid();
            var dalCategory = _mapper.Map<DAL.Models.Category>(category);
            dalCategory.Id = categoryId;
            var blCategory = _mapper.Map<Category>(category);

            _dbContext.Categories.Add(dalCategory);

            await _dbContext.SaveChangesAsync();

            return ServiceDataResponse<Category>.Succeeded(blCategory);
        }

        public async Task<ServiceResponse> DeleteCategoryAsync(Guid categoryId)
        {
            var dalCategory = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
            if (dalCategory == null)
            {
                return ServiceResponse.Failed("Category doesnt exist");
            }

            _dbContext.Categories.Remove(dalCategory);

            dalCategory.IsDeleted= true;

            await _dbContext.SaveChangesAsync();

            return ServiceResponse.Succeeded();
        }

        public async Task<ServiceDataResponse<Category>> GetCategoryByIdAsync(Guid categoryId)
        {
            var dalCategory = await _dbContext.Categories.SingleOrDefaultAsync(c => c.Id == categoryId);

            if (dalCategory==null)
            {
                return ServiceDataResponse<Category>.Failed("Category with this Id doesnt exist");
            }

            var blCategory=_mapper.Map<Category>(dalCategory);

            return ServiceDataResponse<Category>.Succeeded(blCategory);
        }

        public async Task<ServiceDataResponse<IEnumerable<Category>>> GetCategoriesAsync()
        {
            var categories = await _dbContext.Categories.ToListAsync();
            if (categories == null)
            {
                return ServiceDataResponse<IEnumerable<Category>>.Failed("Categories doesnt exist");
            }

            var blCategories = _mapper.Map<IEnumerable<Category>>(categories);

            return ServiceDataResponse<IEnumerable<Category>>.Succeeded(blCategories);
        }

        public async Task<ServiceDataResponse<Category>> UpdateCategoryAsync(Category category)
        {
            var dalCategory = await _dbContext.Categories.FirstOrDefaultAsync(c=>c.Id==category.Id);

            if (dalCategory==null)
            {
                return ServiceDataResponse<Category>.Failed("Category doesnt exist");
            }

            _dbContext.Categories.Update(dalCategory);

            await _dbContext.SaveChangesAsync();

            var blCategory = _mapper.Map<Category>(dalCategory);

            return ServiceDataResponse<Category>.Succeeded(blCategory);
        }
    }
}
