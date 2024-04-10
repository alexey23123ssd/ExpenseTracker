using AutoMapper;
using BL.Services.Interfaces;
using DTO;
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
        public IActionResult CreateCategory([FromBody]Category category)
        {
            return Json(_categoryService.CreateCategory(_mapper.Map<BL.Models.Category>(category)));
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteCategory([FromQuery] Guid id)
        {
            return Json(_categoryService.DeleteCategory(id));
        }

        [HttpGet]
        [Route("get")]
        public IActionResult GetCategory([FromQuery] Guid id)
        {
            return Json(_categoryService.GetCategoryById(id));
        }
    }
}
