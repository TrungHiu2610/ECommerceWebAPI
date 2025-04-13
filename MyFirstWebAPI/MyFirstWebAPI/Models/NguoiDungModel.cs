using System.ComponentModel.DataAnnotations;

namespace MyFirstWebAPI.Models
{
    public class NguoiDungModel
    {
        [Required]
        [MaxLength(50)]
        public string TenDangNhap { get; set; }
        [Required]
        [MaxLength(250)]
        public string MatKhau
        {
            get; set;
        }
    }
}
