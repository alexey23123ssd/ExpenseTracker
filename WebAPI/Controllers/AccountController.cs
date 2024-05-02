using AutoMapper;
using BL.Services.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;

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
        public async Task<IActionResult> CreateAccount([FromBody] DTO.Account account)
        {
            var result = await _accountService.CreateAccountAsync(_mapper.Map<BL.Models.Account>(account));

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Json(result);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteAccount([FromQuery] Guid id)
        {
            var result = await _accountService.DeleteAccountAsync(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAccount([FromQuery] Guid id)
        {
            var result = await _accountService.GetAccountByIdAsync(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return Json(result);
        }

        [HttpGet]
        [Route("getAccs")]
        public async Task<IActionResult> GetAccounts()
        {
            var result = await _accountService.GetAccountsAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return Json(result);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateAccount([FromQuery] DTO.Account account)
        {
            var result = await _accountService.UpdateAccountAsync(_mapper.Map<BL.Models.Account>(account));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Json(result);
        }
    }
}
