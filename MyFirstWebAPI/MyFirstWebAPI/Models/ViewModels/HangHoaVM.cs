namespace MyFirstWebAPI.Models.ViewModels
{
    public class HangHoaVM
    {
        public string TenHH { get; set; }
        public double DonGia { get; set; }
        public HangHoaVM() { }
        public HangHoaVM(string TenHH, double DonGia)
        {
            this.TenHH = TenHH;
            this.DonGia = DonGia;
        }
    }
}
