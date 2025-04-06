using Microsoft.EntityFrameworkCore;
using MyFirstWebAPI.Data;
using MyFirstWebAPI.Models.ViewModels;

namespace MyFirstWebAPI.Services
{
    public class LoaiRepository : ILoaiRepository
    {
        private readonly MyDbContext _context;

        public LoaiRepository(MyDbContext context)
        {
            _context = context;
        }

        public LoaiVM CreateNew(LoaiModel model)
        {
            try
            {
                Loai loai = new Loai
                {
                    TenLoai = model.TenLoai
                };
                _context.Loais.Add(loai);
                _context.SaveChanges();
                return new LoaiVM
                {
                    MaLoai = loai.MaLoai,
                    TenLoai = loai.TenLoai
                };
            }
            catch
            {
                return null;
            }
        }

        public void Delete(int id)
        {
            Loai loai = _context.Loais.SingleOrDefault(p => p.MaLoai == id);
            if(loai!=null)
            {
                _context.Loais.Remove(loai);
                _context.SaveChanges();
            }
        }

        public ICollection<LoaiVM> GetAll()
        {
            return _context.Loais.Select(lo => new LoaiVM
            {
                MaLoai = lo.MaLoai,
                TenLoai = lo.TenLoai
            }).ToList();
        }

        public LoaiVM GetById(int id)
        {
            Loai loai = _context.Loais.SingleOrDefault(p => p.MaLoai == id);
            if (loai != null)
            {
                return new LoaiVM 
                {
                    MaLoai = loai.MaLoai,
                    TenLoai = loai.TenLoai
                };
            }
            return null;
        }

        public void Update(int id, LoaiVM model)
        {
            Loai loai = _context.Loais.SingleOrDefault(p => p.MaLoai == id);
            if (loai != null)
            {
                loai.TenLoai = model.TenLoai;
                _context.Loais.Update(loai);
                _context.SaveChanges();
            }
        }
    }
}
