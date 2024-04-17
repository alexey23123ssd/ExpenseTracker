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
             _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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

            if(await _dbContext.Accounts.AnyAsync(a =>  a.Name == account.Name))
            {
                return new ServiceDataResponse<Account>
                {
                    ErrorMessage = "Account with this Name already exists",
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
            var dalAccount = await _dbContext.Accounts.SingleOrDefaultAsync(a => a.Id == id);
            if (dalAccount == null)
            {
                return new ServiceDataResponse<Guid>
                {
                    ErrorMessage = "Account with this Id doesnt exists",
                    IsSuccess = false,
                };
            }

            _dbContext.Accounts.Remove(dalAccount);

            dalAccount.IsDeleted = true;
            await _dbContext.SaveChangesAsync();

            return new ServiceDataResponse<Guid>
            {
                IsSuccess = true,
                Data = id,
            };
        }

        public async Task<ServiceDataResponse<Account>> GetAccountByIdAsync(Guid id)
        {
            var dalAccount = await _dbContext.Accounts.SingleOrDefaultAsync(a=>a.Id == id);

            if(dalAccount == null)
            {
                return new ServiceDataResponse<Account>
                {
                    ErrorMessage = "Account doesnt exsist",
                    IsSuccess = false,
                };
            }

            var blAccount = _mapper.Map<Account>(dalAccount);

            return new ServiceDataResponse<Account>
            {
                IsSuccess = true,
                Data = blAccount,
            };
        }

        public async Task<ServiceDataResponse<IEnumerable<Account>>> GetAccountsAsync()
        {
            var accounts = await _dbContext.Accounts.ToListAsync();

            if(accounts == null)
            {
                return new ServiceDataResponse<IEnumerable<Account>>
                {
                    ErrorMessage = "Accounts doesnt exsist",
                    IsSuccess = false,
                };
            }

            var blAccounts = _mapper.Map<IEnumerable<Account>>(accounts);

            return new ServiceDataResponse<IEnumerable<Account>>
            {
                IsSuccess = true,
                Data = blAccounts
            };
        }

        public async Task<ServiceDataResponse<Account>> UpdateAccountAsync(Account account)
        {
            var dalAccount = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == account.Id);

            if (dalAccount == null)
            {
                return new ServiceDataResponse<Account>
                {
                    ErrorMessage = "Category doesnt exist",
                    IsSuccess = false
                };
            }

            _dbContext.Accounts.Update(dalAccount);

            await _dbContext.SaveChangesAsync();

            var blAccount = _mapper.Map<Account>(dalAccount);

            return new ServiceDataResponse<Account>()
            {
                IsSuccess = true,
                Data = blAccount
            };
        }
    }
}
