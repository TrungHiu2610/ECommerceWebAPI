using System.ComponentModel.DataAnnotations;

namespace MyFirstWebAPI.Data
{
    public class DonHang
    {
        public enum eTinhTrangDonHang
        {
            New = 0, Cancel = -1, Payment = 1, Complete = 2
        }
        public Guid MaDh { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime? NgayGiao { get; set; }
        public string NguoiNhan { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public eTinhTrangDonHang TinhTrangDonHang { get; set; }

        public ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        public DonHang()
        {
            ChiTietDonHangs = new List<ChiTietDonHang>();
        }

    }
}
