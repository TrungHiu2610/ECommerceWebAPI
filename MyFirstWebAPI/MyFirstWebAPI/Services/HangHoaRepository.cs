using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MyFirstWebAPI.Data;
using MyFirstWebAPI.Helpers;
using MyFirstWebAPI.Models;
using MyFirstWebAPI.Models.ViewModels;
using System.Linq;

namespace MyFirstWebAPI.Services
{
    public class HangHoaRepository : IHangHoaRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;
        public int PAGE_SIZE { get; set; } = 5;

        public HangHoaRepository(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public HangHoaVM CreateNew(HangHoaModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Dữ liệu hàng hóa không được null.");
            }

            var loai = _context.Loais.FirstOrDefault(l => l.MaLoai == model.MaLoai);
            if (loai == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy loại với mã {model.MaLoai}");
            }

            HangHoa hh = _mapper.Map<HangHoa>(model);
            hh.MaHH = Guid.NewGuid();
            hh.Loai = loai;

            _context.HangHoas.Add(hh);
            _context.SaveChanges();

            return _mapper.Map<HangHoaVM>(hh);
        }

        public void Delete(string id)
        {
            if (!Guid.TryParse(id, out Guid _id))
            {
                throw new FormatException("ID không hợp lệ");
            }
            HangHoa hh = _context.HangHoas.SingleOrDefault(h => h.MaHH == _id);
            if (hh == null)
            {
                throw new KeyNotFoundException("Không tìm thấy hàng hóa");
            }
            _context.HangHoas.Remove(hh);
            _context.SaveChanges();
        }

        public ICollection<HangHoaVM> GetAll(string? search, double? from, double? to, string? sortBy, int page = 1)
        {
            var hangHoas = _context.HangHoas.AsQueryable();

            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                hangHoas = hangHoas.Where(h => h.TenHH.Contains(search));
            }
            if (from.HasValue)
            {
                hangHoas = hangHoas.Where(h => h.DonGia >= from);
            }
            if (to.HasValue)
            {
                hangHoas = hangHoas.Where(h => h.DonGia <= to);
            }
            #endregion

            #region Sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "tenhh_asc":
                        hangHoas = hangHoas.OrderBy(h => h.TenHH);
                        break;
                    case "tenhh_desc":
                        hangHoas = hangHoas.OrderByDescending(h => h.TenHH);
                        break;
                    case "dongia_asc":
                        hangHoas = hangHoas.OrderBy(h => h.DonGia);
                        break;
                    case "dongia_desc":
                        hangHoas = hangHoas.OrderByDescending(h => h.DonGia);
                        break;
                }
            }
            #endregion

            #region Paging
            // phân trang thủ công
            //hangHoas = hangHoas.Skip((int)((page - 1) * PAGE_SIZE)).Take(PAGE_SIZE);

            // phân trang bằng PaginatedList<T>
            // Chuyển IQueryable<HangHoa> thành IQueryable<HangHoaVM> TRƯỚC khi phân trang
            var hangHoaVMs = hangHoas.ProjectTo<HangHoaVM>(_mapper.ConfigurationProvider);

            // Phân trang
            var result = PaginatedList<HangHoaVM>.Create(hangHoaVMs, page, PAGE_SIZE);

            #endregion

            return result;
        }

        public HangHoaVM GetById(string id)
        {
            if (!Guid.TryParse(id, out Guid _id))
            {
                throw new FormatException("ID không hợp lệ");
            }
            return _mapper.Map<HangHoaVM>(_context.HangHoas.SingleOrDefault(p => p.MaHH == _id));
        }

        public void Update(string id, HangHoaModel model)
        {
            if (!Guid.TryParse(id, out Guid _id))
                throw new FormatException("ID không hợp lệ");

            var hh = _context.HangHoas.SingleOrDefault(h => h.MaHH == _id);
            if (hh == null)
                throw new KeyNotFoundException("Không tìm thấy hàng hóa cần cập nhật");

            _mapper.Map(model, hh);

            var loai = _context.Loais.SingleOrDefault(l => l.MaLoai == model.MaLoai);
            if (loai == null)
                throw new KeyNotFoundException("Không tìm thấy loại hàng hóa");

            hh.Loai = loai;

            _context.SaveChanges();
        }
    }
}
