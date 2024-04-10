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
        ServiceDataResponse<IEnumerable<Expense>> GetExpenses(Category? category,Account? account);
        ServiceDataResponse<Expense>GetExpenseById(Guid id);
        ServiceDataResponse<Guid>CreateExpense(Expense expense);
        ServiceDataResponse<Expense> UpdateExpense(Expense expense);
        ServiceResponse DeleteExpense(Expense expense);
    }
}
