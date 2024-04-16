using AutoMapper;
using BL.Models;
using DALCategory = DAL.Models.Category;
using DALAccount = DAL.Models.Account;
using DALExpense = DAL.Models.Expense;

namespace BL.Automapper
{
    public class DtoBlMappingProfile:Profile
    {
        public DtoBlMappingProfile()
        {
            CreateMap<Category, DALCategory>();
            CreateMap<DALCategory,Category>();
            CreateMap<IEnumerable<DALCategory>, IEnumerable<Category>>();
            CreateMap<Account, DALAccount>();
            CreateMap<DALAccount, Account>();
            CreateMap<Expense, DALExpense>();
            CreateMap<DALExpense, Expense>();
        }
    }
}
