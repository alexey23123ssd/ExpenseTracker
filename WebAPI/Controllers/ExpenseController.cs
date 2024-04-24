using AutoMapper;
using BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace WebAPI.Controllers
{

    [Route("[controller]")]
    public class ExpenseController : Controller
    {
        private readonly IExpenseService _expenseService;
        private readonly IMapper _mapper;

        public ExpenseController(IExpenseService expenseService, IMapper mapper)
        {
            _expenseService = expenseService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateExpense([FromBody] DTO.Expense expense)
        {
            return Json(await _expenseService.CreateExpenseAsync(_mapper.Map<BL.Models.Expense>(expense)));
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteExpense([FromQuery] Guid id)
        {
            return Json(await _expenseService.DeleteExpenseAsync(id));
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetExpense([FromQuery] Guid id)
        {
            return Json(await _expenseService.GetExpenseByIdAsync(id));
        }

        [HttpPut]
        [Route("put")]
        public async Task<IActionResult> UpdateExpense([FromQuery] DTO.Expense expense)
        {
            var blExpense = _mapper.Map<BL.Models.Expense>(expense);
            return Json(await _expenseService.UpdateExpenseAsync(blExpense));
        }
    }
}
