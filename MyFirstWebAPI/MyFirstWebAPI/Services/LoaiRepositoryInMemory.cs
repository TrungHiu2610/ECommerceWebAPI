using MyFirstWebAPI.Data;
using MyFirstWebAPI.Models.ViewModels;

namespace MyFirstWebAPI.Services
{
    public class LoaiRepositoryInMemory : ILoaiRepository
    {
        static List<LoaiVM> Loais = new List<LoaiVM>
        {
            new LoaiVM{MaLoai = 1, TenLoai = "Coffee"},
            new LoaiVM{MaLoai = 2, TenLoai = "Milk Tea"},
            new LoaiVM{MaLoai = 3, TenLoai = "Soda"},
            new LoaiVM{MaLoai = 4, TenLoai = "Latte"},
            new LoaiVM{MaLoai = 5, TenLoai = "Water"},
        };
        public LoaiVM CreateNew(LoaiModel model)
        {
            LoaiVM loai = new LoaiVM
            {
                MaLoai = Loais.Max(lo => lo.MaLoai) + 1,
                TenLoai = model.TenLoai
            };
            Loais.Add(loai);
            return loai;
        }

        public void Delete(int id)
        {
            Loais.Remove(Loais.SingleOrDefault(lo => lo.MaLoai == id));
        }

        public ICollection<LoaiVM> GetAll()
        {
            return Loais;
        }

        public LoaiVM GetById(int id)
        {
            LoaiVM loai = Loais.SingleOrDefault(lo => lo.MaLoai == id);
            if (loai != null)
                return loai;
            return null;
        }

        public void Update(int id, LoaiVM model)
        {
            LoaiVM loai = Loais.SingleOrDefault(lo => lo.MaLoai == id);
            if (loai != null)
            {
                loai.TenLoai = model.TenLoai;
            }
        }
    }
}
