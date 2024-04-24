using BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
