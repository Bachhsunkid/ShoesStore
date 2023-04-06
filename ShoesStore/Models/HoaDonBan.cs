using System;
using System.Collections.Generic;

namespace ShoesStore.Models;

public partial class HoaDonBan
{
    public string MaHdb { get; set; } = null!;

    public string? MaNv { get; set; }

    public string? MaKh { get; set; }

    public DateTime? NgayBan { get; set; }

    public byte? TrangThai { get; set; }

    public byte? PhuongThucThanhToan { get; set; }

    public decimal? TongTien { get; set; }

    public string? GhiChu { get; set; }

    public virtual ICollection<ChiTietHdb> ChiTietHdbs { get; } = new List<ChiTietHdb>();

    public virtual KhachHang? MaKhNavigation { get; set; }

    public virtual NhanVien? MaNvNavigation { get; set; }
}
