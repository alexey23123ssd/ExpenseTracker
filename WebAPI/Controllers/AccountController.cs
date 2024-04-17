using AutoMapper;
using BL.Services.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAccount([FromBody] Account account)
        {
            return Json(await _accountService.CreateAccountAsync(_mapper.Map<BL.Models.Account>(account)));
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteAccount([FromQuery] Guid id)
        {
            return Json(await _accountService.DeleteAccountAsync(id));
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAccount([FromQuery] Guid id)
        {
            return Json(await _accountService.GetAccountByIdAsync(id));
        }

        [HttpPut]
        [Route("put")]
        public async Task<IActionResult> UpdateAccount([FromQuery] Account account)
        {
            var blAccount = _mapper.Map<BL.Models.Account>(account);
            return Json(await _accountService.UpdateAccountAsync(blAccount));
        }


    }
}
