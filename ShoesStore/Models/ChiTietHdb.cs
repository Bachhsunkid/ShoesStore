using System;
using System.Collections.Generic;

namespace ShoesStore.Models;

public partial class ChiTietHdb
{
    public string MaHdb { get; set; } = null!;

    public string MaGiay { get; set; } = null!;

    public int? SoLuong { get; set; }

    public decimal? KhuyenMai { get; set; }

    public virtual Giay MaGiayNavigation { get; set; } = null!;

    public virtual HoaDonBan MaHdbNavigation { get; set; } = null!;
}
