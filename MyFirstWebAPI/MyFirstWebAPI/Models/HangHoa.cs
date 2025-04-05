using MyFirstWebAPI.Models.ViewModels;

namespace MyFirstWebAPI.Models
{
    public class HangHoa : HangHoaVM
    {
        public Guid MaHH { get; set; }

        public HangHoa():base() { }

        public HangHoa(string tenHH, double donGia) : base(tenHH, donGia)
        {
            MaHH = Guid.NewGuid();
        }
    }
}
