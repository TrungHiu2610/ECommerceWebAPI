using MyFirstWebAPI.Data;
using MyFirstWebAPI.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstWebAPI.Models
{
    public class HangHoaModel
    {
        public string TenHH { get; set; }
        public string MoTa { get; set; }
        public double DonGia { get; set; }
        public byte GiamGia { get; set; }
        public int? MaLoai { get; set; }
    }
}
