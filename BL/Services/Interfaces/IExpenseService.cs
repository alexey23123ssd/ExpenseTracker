using BL.Models;

namespace BL.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<ServiceDataResponse<IEnumerable<Expense>>> GetExpensesAsync(Account? account);
        Task<ServiceDataResponse<Expense>> GetExpenseByIdAsync(Guid id);
        Task<ServiceDataResponse<Expense>> CreateExpenseAsync(Expense expense);
        Task<ServiceDataResponse<Expense>> UpdateExpenseAsync(Expense expense);
        Task<ServiceResponse> DeleteExpenseAsync(Guid id);
    }
}
