using AutoMapper;
using BL.Models;
namespace WebAPI.Automapper
{
    public class DalBlMappingProfile : Profile
    {
        public DalBlMappingProfile()
        {
            CreateMap<DTO.Category, Category>();
            CreateMap<Category, DTO.Category>();
        }
    }
    
}
