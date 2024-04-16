using AutoMapper;
using BL.Models;
using BL.Services.Interfaces;
using DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class AccountService:IAccountService
    {
        private readonly ExpenseTrackerDbContext _dbContext;
        private readonly IMapper _mapper;

        public AccountService(ExpenseTrackerDbContext dbContext, IMapper mapper)
        {
             _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));    
             _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        public async Task<ServiceDataResponse<Account>> CreateAccountAsync(Account account)
        {
            if(account == null)
            {
                return new ServiceDataResponse<Account>
                {
                    ErrorMessage = "Data cannot be null",
                    IsSuccess = false,
                };
            }

            if(await _dbContext.Accounts.AnyAsync(a =>  a.Id == account.Id))
            {
                return new ServiceDataResponse<Account>
                {
                    ErrorMessage = "Account with this Id already exists",
                    IsSuccess = false,
                };
            }

            var accountId = Guid.NewGuid();
            var dalAccount = _mapper.Map<DAL.Models.Account>(account);
            dalAccount.Id = accountId;
            var blAccount = _mapper.Map<Account>(account);

            _dbContext.Accounts.Add(dalAccount);
            await _dbContext.SaveChangesAsync();

            return new ServiceDataResponse<Account>
            {
                IsSuccess = true,
                Data = blAccount,
            };
        }

        public async Task<ServiceResponse> DeleteAccountAsync(Guid id)
        {
            if (!await _dbContext.Accounts.AnyAsync(a => a.Id == id))
            {
                return new ServiceDataResponse<Guid>
                {
                    ErrorMessage = "Account with this Id doesnt exists",
                    IsSuccess = false,
                };
            }
            var dalAccount = _mapper.Map<DAL.Models.Account>(id);
            _dbContext.Accounts.Remove(dalAccount);

            dalAccount.IsDeleted = true;
            await _dbContext.SaveChangesAsync();

            return new ServiceDataResponse<Guid>
            {
                IsSuccess = true,
                Data = id,
            };
        }

        public Task<ServiceDataResponse<Account>> GetAccountByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceDataResponse<IEnumerable<Account>>> GetAccountsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceDataResponse<Account>> UpdateAccountAsync(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
