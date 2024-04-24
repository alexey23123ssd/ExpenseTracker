using AutoMapper;
using BL.Services;
using BL.Services.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper= mapper;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateCategory([FromBody] DTO.Category category)
        {
            return Json(await _categoryService.CreateCategoryAsync(_mapper.Map<BL.Models.Category>(category)));
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteCategory([FromQuery] Guid id)
        {
            return Json(await _categoryService.DeleteCategoryAsync(id));
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetCategory([FromQuery] Guid id)
        {
            return Json(await _categoryService.GetCategoryByIdAsync(id));
        }

        [HttpPut]
        [Route("put")]
        public async Task<IActionResult> UpdateCategory([FromQuery] DTO.Category category)
        {
            var blCategory = _mapper.Map<BL.Models.Category>(category);
            return Json(await _categoryService.UpdateCategoryAsync(blCategory));
        }
    }
}
