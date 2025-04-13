using System.ComponentModel.DataAnnotations;

namespace MyFirstWebAPI.Data
{
    public class NguoiDung
    {
        [Key]
        public int MaND { get; set; }
        [Required]
        [MaxLength(50)]
        public string TenDangNhap { get; set; }
        [Required]
        [MaxLength(250)]
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
    }
}
