using AutoMapper;
using BL.Models;
using DALCategory = DAL.Models.Category;

namespace BL.Automapper
{
    public class DtoBlMappingProfile:Profile
    {
        public DtoBlMappingProfile()
        {
            CreateMap<Category, DALCategory>();
            CreateMap<DALCategory,Category>();
            CreateMap<IEnumerable<DALCategory>, IEnumerable<Category>>();
        }
    }
}
