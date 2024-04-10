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
        public ServiceDataResponse<Guid> CreateCategory(Category category)
        {
            if(category == null)
            {
               return new ServiceDataResponse<Guid>
                {
                    ErrorMessage = "Data cannot be null",
                    IsSuccess = false
                };
            }
            if(_dbContext.Categories.Any(c => c.Name == category.Name))
            {
                return new ServiceDataResponse<Guid>
                {
                    ErrorMessage = "Category with this name already exist",
                    IsSuccess = false
                };
            }
            var categoryId=Guid.NewGuid();
            var dalCategory = _mapper.Map<DAL.Models.Category>(category);
            dalCategory.Id = categoryId;
            _dbContext.Categories.Add(dalCategory);
            _dbContext.SaveChanges();
            return new ServiceDataResponse<Guid>
            {
                IsSuccess = true,
                Data = categoryId,
            };
        }

        public ServiceResponse DeleteCategory(Guid categoryId)
        {
            if (!_dbContext.Categories.Any(c => c.Id == categoryId))
            {
                return new ServiceDataResponse<Guid>
                {
                    ErrorMessage = "Category doesn't exist",
                    IsSuccess = false
                };
            }

            var dalCategory = _mapper.Map<DAL.Models.Category>(categoryId);
            _dbContext.Categories.Remove(dalCategory);

            dalCategory.IsDeleted= true;

            _dbContext.SaveChanges();

            return new ServiceDataResponse<Guid>
            {
                IsSuccess = true,
                Data = categoryId,
            };
        }

        public ServiceDataResponse<Category> GetCategoryById(Guid categoryId)
        {
            if(!_dbContext.Categories.Any(c => c.Id == categoryId))
            {
                return new ServiceDataResponse<Category>
                {
                    ErrorMessage = "Category doesn't exist",
                    IsSuccess = false
                };
            }

            var dalCategory = _mapper.Map<DAL.Models.Category>(categoryId);
            var result=_dbContext.Categories.SingleOrDefault(c => c.Id == categoryId);
            var blCategory=_mapper.Map<Category>(result);

            return new ServiceDataResponse<Category>
            {
                IsSuccess = true,
                Data = blCategory
            };
        }

        public ServiceDataResponse<IEnumerable<Category>> GetCategories()
        {
            if (!_dbContext.Categories.Any())
            {
                return new ServiceDataResponse<IEnumerable<Category>>
                {
                    ErrorMessage = "Account doesnt have any categories",
                    IsSuccess = false
                };
            }

            var categories = _dbContext.Categories.ToList();
            var blCategories = _mapper.Map<IEnumerable<Category>>(categories);

            return new ServiceDataResponse<IEnumerable<Category>>() 
            {
                IsSuccess = true,
                Data = blCategories
            };
        }

        public ServiceDataResponse<Category> UpdateCategory(Category Category)
        {
            if(!_dbContext.Categories.Any(c=>c.Id == Category.Id))
            {
                return new ServiceDataResponse<Category>
                {
                    ErrorMessage = "Category doesnt exist",
                    IsSuccess = false
                };
            }

            var dalCategory = _mapper.Map<DAL.Models.Category>(Category);
            var result = _dbContext.Categories.SingleOrDefault(c => c.Id == Category.Id);
            _dbContext.Categories.Update(result);
            _dbContext.SaveChanges();
            var blCategory = _mapper.Map<Category>(result);

            return new ServiceDataResponse<Category>()
            {
                IsSuccess = true,
                Data = blCategory
            };
        }
    }
}
