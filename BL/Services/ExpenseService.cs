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
                return ServiceDataResponse<Expense>.Failed("Data cannot be null");
            }

            if (await _dbContext.Accounts.AnyAsync(e => e.Id == expense.Id))
            {
                return ServiceDataResponse<Expense>.Failed("Expense with this id already exist");
            }

            var expenseId = Guid.NewGuid();
            var dalExpense = _mapper.Map<DAL.Models.Expense>(expense);
            dalExpense.Id = expenseId;
            var blExpense = _mapper.Map<Expense>(expense);

            _dbContext.Expenses.Add(dalExpense);
            await _dbContext.SaveChangesAsync();

            return ServiceDataResponse<Expense>.Succeeded(blExpense);
        }

        public async Task<ServiceResponse> DeleteExpenseAsync(Guid id)
        {
            var dalExpense = await _dbContext.Accounts.SingleOrDefaultAsync(e => e.Id == id);

            if (dalExpense == null)
            {
                return ServiceResponse.Failed("Expense with this id doesnt exist");
            }

            _dbContext.Accounts.Remove(dalExpense);

            dalExpense.IsDeleted = true;
            await _dbContext.SaveChangesAsync();

            return ServiceResponse.Succeeded();
        }

        public async Task<ServiceDataResponse<Expense>> GetExpenseByIdAsync(Guid id)
        {
            var dalExpense = await _dbContext.Expenses.SingleOrDefaultAsync(e => e.Id == id);

            if (dalExpense == null)
            {
                return ServiceDataResponse<Expense>.Failed("Account with this Id doesnt exist");
            }

            var blExpense = _mapper.Map<Expense>(dalExpense);

            return ServiceDataResponse<Expense>.Succeeded(blExpense);
        }

        public async Task<ServiceDataResponse<IEnumerable<Expense>>> GetExpensesAsync(Models.Account? account)
        {
            var expenses = await _dbContext.Expenses.ToListAsync();

            if (expenses == null)
            {
                return ServiceDataResponse<IEnumerable<Expense>>.Failed("Expenses doesnt exist");
            }

            var blExpenses = _mapper.Map<IEnumerable<Expense>>(expenses);

            return ServiceDataResponse<IEnumerable<Expense>>.Succeeded(blExpenses);
        }


        public async Task<ServiceDataResponse<Expense>> UpdateExpenseAsync(Expense expense)
        {
            var dalExpense = await _dbContext.Expenses.FirstOrDefaultAsync(a => a.Id == expense.Id);

            if (dalExpense == null)
            {
                return ServiceDataResponse<Expense>.Failed("Expense doesnt exist");
            }

            _dbContext.Expenses.Update(dalExpense);

            await _dbContext.SaveChangesAsync();

            var blExpense = _mapper.Map<Expense>(dalExpense);

            return ServiceDataResponse<Expense>.Succeeded(blExpense);
        }
    }
}
