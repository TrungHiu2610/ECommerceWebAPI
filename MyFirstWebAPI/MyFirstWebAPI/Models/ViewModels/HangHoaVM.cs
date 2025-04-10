using MyFirstWebAPI.Data;

namespace MyFirstWebAPI.Models.ViewModels
{
    public class HangHoaVM
    {
        public Guid MaHH { get; set; }
        public string TenHH { get; set; }
        public double DonGia { get; set; }
        public string MoTa { get; set; }
        public string MaLoai { get; set; }
    }
}
