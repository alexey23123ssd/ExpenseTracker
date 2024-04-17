using AutoMapper;
using BL.Models;
using BL.Services.Interfaces;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Expense = BL.Models.Expense;

namespace BL.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly ExpenseTrackerDbContext _dbContext;
        private readonly IMapper _mapper;

        public ExpenseService(ExpenseTrackerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));   
        }
        public async Task<ServiceDataResponse<Expense>> CreateExpenseAsync(Expense expense)
        {
            if (expense == null)
            {
                return new ServiceDataResponse<Expense>
                {
                    ErrorMessage = "Data cannot be null",
                    IsSuccess = false,
                };
            }

            var expenseId = Guid.NewGuid();
            var dalExpense = _mapper.Map<DAL.Models.Expense>(expense);
            dalExpense.Id = expenseId;
            var blExpense = _mapper.Map<Expense>(expense);

            _dbContext.Expenses.Add(dalExpense);
            await _dbContext.SaveChangesAsync();

            return new ServiceDataResponse<Expense>
            {
                IsSuccess = true,
                Data = blExpense,
            };
        }

        public async Task<ServiceResponse> DeleteExpenseAsync(Guid id)
        {
            var dalExpense = await _dbContext.Accounts.SingleOrDefaultAsync(e => e.Id == id);
            if (dalExpense == null)
            {
                return new ServiceDataResponse<Guid>
                {
                    ErrorMessage = "Expense with this Id doesnt exists",
                    IsSuccess = false,
                };
            }

            _dbContext.Accounts.Remove(dalExpense);

            dalExpense.IsDeleted = true;
            await _dbContext.SaveChangesAsync();

            return new ServiceDataResponse<Guid>
            {
                IsSuccess = true,
                Data = id,
            };
        }

        public async Task<ServiceDataResponse<Expense>> GetExpenseByIdAsync(Guid id)
        {
            var dalExpense = await _dbContext.Expenses.SingleOrDefaultAsync(e => e.Id == id);

            if (dalExpense == null)
            {
                return new ServiceDataResponse<Expense>
                {
                    ErrorMessage = "Expense  with this Id doesnt exists",
                    IsSuccess = false,
                };
            }

            var blExpense = _mapper.Map<Expense>(dalExpense);

            return new ServiceDataResponse<Expense>
            {
                IsSuccess = true,
                Data = blExpense,
            };
        }

        public async Task<ServiceDataResponse<IEnumerable<Expense>>> GetExpensesAsync(Models.Account? account)
        {
            var expenses = await _dbContext.Expenses.ToListAsync();

            if (expenses == null)
            {
                return new ServiceDataResponse<IEnumerable<Expense>>
                {
                    ErrorMessage = "Expenses doesnt exsist",
                    IsSuccess = false,
                };
            }

            var blExpenses = _mapper.Map<IEnumerable<Expense>>(expenses);

            return new ServiceDataResponse<IEnumerable<Expense>>
            {
                IsSuccess = true,
                Data = blExpenses
            };
        }

        public async Task<ServiceDataResponse<Expense>> UpdateExpenseAsync(Expense expense)
        {
            var dalExpense = await _dbContext.Expenses.FirstOrDefaultAsync(a => a.Id == expense.Id);

            if (dalExpense == null)
            {
                return new ServiceDataResponse<Expense>
                {
                    ErrorMessage = "Expense doesnt exist",
                    IsSuccess = false
                };
            }

            _dbContext.Expenses.Update(dalExpense);

            await _dbContext.SaveChangesAsync();

            var blExpense = _mapper.Map<Expense>(dalExpense);

            return new ServiceDataResponse<Expense>()
            {
                IsSuccess = true,
                Data = blExpense
            };
        }
    }
}
