using System;
using System.Collections.Generic;

namespace ShoesStore.Models;

public partial class Giay
{
    public string MaGiay { get; set; } = null!;

    public string MaLoai { get; set; } = null!;

    public string? TenGiay { get; set; }

    public byte? KichCo { get; set; }

    public string? MauSac { get; set; }

    public int? SoLuong { get; set; }

    public decimal? GiaNhap { get; set; }

    public decimal? GiaGoc { get; set; }

    public decimal? GiaBan { get; set; }

    public double? PhanTramGiam { get; set; }

    public string? TinhTrang { get; set; }

    public double? DanhGia { get; set; }

    public string? AnhDaiDien { get; set; }

    public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; } = new List<ChiTietGioHang>();

    public virtual ICollection<ChiTietHdb> ChiTietHdbs { get; } = new List<ChiTietHdb>();

    public virtual ICollection<ChiTietHdn> ChiTietHdns { get; } = new List<ChiTietHdn>();

    public virtual LoaiGiay MaLoaiNavigation { get; set; } = null!;
}
