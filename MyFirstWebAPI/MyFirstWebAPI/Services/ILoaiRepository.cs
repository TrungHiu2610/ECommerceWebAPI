using MyFirstWebAPI.Data;
using MyFirstWebAPI.Models.ViewModels;

namespace MyFirstWebAPI.Services
{
    public interface ILoaiRepository
    {
        ICollection<LoaiVM> GetAll();
        LoaiVM GetById(int id);
        LoaiVM CreateNew(LoaiModel model);
        void Update(int id, LoaiVM model);
        void Delete(int id);
    }
}
