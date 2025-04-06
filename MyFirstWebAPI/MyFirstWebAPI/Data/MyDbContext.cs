using Microsoft.EntityFrameworkCore;

namespace MyFirstWebAPI.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }

        #region Db Set
        public DbSet<HangHoa> HangHoas { get; set; }
        public DbSet<Loai> Loais { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DonHang>(entity =>
            {
                entity.ToTable("DonHang");
                entity.HasKey(e => e.MaDh);
                entity.Property(e => e.NgayDat).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.NguoiNhan).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<ChiTietDonHang>(entity =>
            {
                entity.ToTable("ChiTietDonHang");
                entity.HasKey(e => new { e.MaDh, e.MaHH });
                entity.HasOne(e => e.DonHang)
                    .WithMany(e=>e.ChiTietDonHangs)
                    .HasForeignKey(e=>e.MaDh)
                    .HasConstraintName("FK_ChiTietDonHang_DonHang");

                entity.HasOne(e => e.HangHoa)
                   .WithMany(e => e.ChiTietDonHangs)
                   .HasForeignKey(e => e.MaHH)
                   .HasConstraintName("FK_ChiTietDonHang_HangHoa");
            });
        }
    }
}
