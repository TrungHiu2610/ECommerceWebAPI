using System.ComponentModel.DataAnnotations;

namespace MyFirstWebAPI.Models.ViewModels
{
    public class LoaiModel
    {
        [Required]
        [MaxLength(100)]
        public string TenLoai { get; set; }
    }
}
