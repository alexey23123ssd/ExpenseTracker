using BL.Models;
namespace BL.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ServiceDataResponse<Account>> CreateAccountAsync(Account account);
        Task<ServiceDataResponse<IEnumerable<Account>>> GetAccountsAsync();
        Task<ServiceDataResponse<Account>> GetAccountByIdAsync(Guid id);
        Task<ServiceDataResponse<Account>> UpdateAccountAsync(Account account);
        Task<ServiceResponse> DeleteAccountAsync(Guid id);
    }
}
