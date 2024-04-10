using BL.Models;
namespace BL.Services.Interfaces
{
    public interface IAccountService
    {
        ServiceDataResponse<Guid> CreateAccount(Account account);
        ServiceDataResponse<IEnumerable<Account>> GetAccounts();
        ServiceDataResponse<Account> GetAccountById(int id);
        ServiceDataResponse<Account> UpdateAccount(Account account);
        ServiceResponse DeleteAccount(Account account);
    }
}
