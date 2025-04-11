using MyFirstWebAPI.Models;
using MyFirstWebAPI.Models.ViewModels;

namespace MyFirstWebAPI.Services
{
    public interface IHangHoaRepository
    {
        ICollection<HangHoaVM> GetAll(string? search, double? from, double? to, string? sortBy, int page = 1);
        HangHoaVM GetById(string id);
        HangHoaVM CreateNew(HangHoaModel model);
        void Delete(string id);
        void Update(string id, HangHoaModel model);
    }
}
