using System;
using System.Collections.Generic;

namespace ShoesStore.Models;

public partial class HoaDonNhap
{
    public string MaHdn { get; set; } = null!;

    public string? MaNv { get; set; }

    public string? MaNcc { get; set; }

    public DateTime? NgayNhap { get; set; }

    public decimal? TongTien { get; set; }

    public virtual ICollection<ChiTietHdn> ChiTietHdns { get; } = new List<ChiTietHdn>();

    public virtual NhaCungCap? MaNccNavigation { get; set; }

    public virtual NhanVien? MaNvNavigation { get; set; }
}
