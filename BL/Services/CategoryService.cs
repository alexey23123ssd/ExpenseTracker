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
               return new ServiceDataResponse<Category>
                {
                    ErrorMessage = "Data cannot be null",
                    IsSuccess = false
                };
            }

            if(await _dbContext.Categories.AnyAsync(c => c.Id == category.Id))
            {
                return new ServiceDataResponse<Category>
                {
                    ErrorMessage = "Category with this Id already exist",
                    IsSuccess = false
                };
            }

            var categoryId = Guid.NewGuid();
            var dalCategory = _mapper.Map<DAL.Models.Category>(category);
            dalCategory.Id = categoryId;
            var blCategory = _mapper.Map<Category>(category);

            _dbContext.Categories.Add(dalCategory);

            await _dbContext.SaveChangesAsync();

            return new ServiceDataResponse<Category>
            {
                IsSuccess = true,
                Data = blCategory,
            };
        }

        public async Task<ServiceResponse> DeleteCategoryAsync(Guid categoryId)
        {
            var dalCategory = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
            if (dalCategory == null)
            {
                return new ServiceDataResponse<Guid>
                {
                    ErrorMessage = "Category doesn't exist",
                    IsSuccess = false
                };
            }

            _dbContext.Categories.Remove(dalCategory);

            dalCategory.IsDeleted= true;

            await _dbContext.SaveChangesAsync();

            return new ServiceDataResponse<Guid>
            {
                IsSuccess = true,
                Data = categoryId,
            };
        }

        public async Task<ServiceDataResponse<Category>> GetCategoryByIdAsync(Guid categoryId)
        {
            var dalCategory = await _dbContext.Categories.SingleOrDefaultAsync(c => c.Id == categoryId);

            if (dalCategory==null)
            {
                return new ServiceDataResponse<Category>
                {
                    ErrorMessage = "Category doesn't exist",
                    IsSuccess = false
                };
            }

            var blCategory=_mapper.Map<Category>(dalCategory);

            return new ServiceDataResponse<Category>
            {
                IsSuccess = true,
                Data = blCategory
            };
        }

        public async Task<ServiceDataResponse<IEnumerable<Category>>> GetCategoriesAsync()
        {
            var categories = await _dbContext.Categories.ToListAsync();
            if (categories == null)
            {
                return new ServiceDataResponse<IEnumerable<Category>>
                {
                    ErrorMessage = "Account doesnt have any categories",
                    IsSuccess = false
                };
            }

            var blCategories = _mapper.Map<IEnumerable<Category>>(categories);

            return new ServiceDataResponse<IEnumerable<Category>>() 
            {
                IsSuccess = true,
                Data = blCategories
            };
        }

        public async Task<ServiceDataResponse<Category>> UpdateCategoryAsync(Category category)
        {
            var dalCategory = await _dbContext.Categories.FirstOrDefaultAsync(c=>c.Id==category.Id);

            if (dalCategory==null)
            {
                return new ServiceDataResponse<Category>
                {
                    ErrorMessage = "Category doesnt exist",
                    IsSuccess = false
                };
            }

            _dbContext.Categories.Update(dalCategory);

            await _dbContext.SaveChangesAsync();

            var blCategory = _mapper.Map<Category>(dalCategory);

            return new ServiceDataResponse<Category>()
            {
                IsSuccess = true,
                Data = blCategory
            };
        }
    }
}
