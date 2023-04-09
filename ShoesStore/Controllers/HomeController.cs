using Microsoft.AspNetCore.Mvc;
using ShoesStore.Models;
using ShoesStore.ViewModels;
using System.Diagnostics;
using X.PagedList;

namespace ShoesStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        Qlbangiaynhom7Context db = new Qlbangiaynhom7Context();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var lstGiay = db.Giays.GroupBy(x => x.TenGiay)
                        .Select(group => group.First())
                        .ToList();

            List<GiayDTO> lstShoesDTO = new List<GiayDTO>();

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
                lstShoesDTO.Add(temp);
            }
            return View(lstShoesDTO);
        }

        public IActionResult Shop(int page = 1)
        {
            int pageNumber = page;
            int pageSize = 3;

            var lstGiay = db.Giays.GroupBy(x => x.TenGiay)
                        .Select(group => group.First())
                        .ToList();

            List<GiayDTO> lstShoesDTO = new List<GiayDTO>();

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
                lstShoesDTO.Add(temp);
            }

            PagedList<GiayDTO> lstShoesDTOPaging = new PagedList<GiayDTO>(lstShoesDTO, pageNumber, pageSize);

            return View(lstShoesDTOPaging);
        }
        public IActionResult Single()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        //public IActionResult Blog()
        //{
        //    return View();
        //}
        //public IActionResult BlogSingle()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}