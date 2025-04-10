using AutoMapper;
using MyFirstWebAPI.Data;
using MyFirstWebAPI.Models;
using MyFirstWebAPI.Models.ViewModels;

namespace MyFirstWebAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<HangHoaModel, HangHoa>();
            CreateMap<HangHoa, HangHoaVM>();
            CreateMap<HangHoaVM, HangHoa>();
        }
    }
}
