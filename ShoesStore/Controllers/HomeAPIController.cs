using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesStore.Models;
using ShoesStore.ViewModels;
using X.PagedList;

namespace ShoesStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeAPIController : ControllerBase
    {
        Qlbangiaynhom7Context db = new Qlbangiaynhom7Context();

        [HttpGet("GetShoeByFilter")]
        public IEnumerable<GiayDTO> GetShoeByFilter(int page = 1)
        {
            int pageNumber = page;
            int pageSize = 3;

            var lstGiay = db.Giays.GroupBy(x => x.TenGiay)
                        .Select(group => group.First())
                        .ToList();

            List<GiayDTO> lstShoesFiltered = new List<GiayDTO>();

            foreach (var item in lstGiay)
            {
                GiayDTO temp = new GiayDTO
                {
                    MaGiay = item.MaGiay,
                    TenLoai = db.LoaiGiays.Find(item.MaLoai).TenLoai,
                    TenGiay = item.TenGiay,
                    KichCo = item.KichCo,
                    MauSac = item.MauSac,
                    GiaGoc = item.GiaGoc,
                    GiaBan = item.GiaBan,
                    PhanTramGiam = item.PhanTramGiam,
                    DanhGia = item.DanhGia,
                    AnhDaiDien = item.AnhDaiDien,
                    SoLuong = item.SoLuong
                };
                lstShoesFiltered.Add(temp);
            }

            return lstShoesFiltered;
        }
    }
}
