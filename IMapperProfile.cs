using AutoMapper;
using FinalExamProject.Areas.Manage.ViewModel.Fruit;
using FinalExamProject.Models;

namespace FinalExamProject
{
    public class IMapperProfile : Profile
    {
        public IMapperProfile()
        {
            CreateMap<FruitCreateVm, Fruit>();
            CreateMap<FruitUpdateVm, Fruit>();
            CreateMap<FruitCreateVm, Fruit>().ReverseMap();

        }
    }
}
