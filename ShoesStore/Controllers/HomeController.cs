using Microsoft.AspNetCore.Mvc;
using ShoesStore.Models;
using ShoesStore.Models.ModelDTOs;
using ShoesStore.ViewModels;
using System.Diagnostics;
using System.Linq;
using ShoesStore.Models.Authentication;
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

        public IActionResult Shop(string? keyword, int? startPrice, int? limitPrice, List<string>? occasionCheckboxes, int page = 1)
        {
            int pageNumber = page;
            int pageSize = 3;
            var lstGiay = db.Giays.GroupBy(x => x.TenGiay)
            .Select(group => group.First())
            .ToList();
            var lstShoesDTO = GetListProduct(lstGiay);

            var temp = lstShoesDTO;

            if (occasionCheckboxes.Count > 0)
            {
                lstShoesDTO = lstShoesDTO.Where(x => occasionCheckboxes.Any(occasion => x.TenLoai.Contains(occasion))).ToList();
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                lstShoesDTO = lstShoesDTO.Where(x => x.TenGiay.ToLower().Contains(keyword.ToLower())).ToList();
            }
            if (startPrice.HasValue)
            {
                lstShoesDTO = lstShoesDTO.Where(x => x.GiaBan >= startPrice).ToList();
            }
            if (limitPrice.HasValue)
            {
                lstShoesDTO = lstShoesDTO.Where(x => x.GiaBan <= limitPrice).ToList();
            }

            ViewBag.pageSize = pageSize;
            ViewBag.keyword = keyword;
            ViewBag.startPrice = startPrice;
            ViewBag.limitPrice = limitPrice;
            ViewBag.occasionCheckboxes = occasionCheckboxes;

            PagedList<GiayDTO> lst = new PagedList<GiayDTO>(lstShoesDTO, pageNumber, pageSize);

            return View(lst);
        }

        [HttpPost]
        public IActionResult GetShoesByFilter(string? keyword, int? startPrice, int? limitPrice, List<string>? occasionCheckboxes, int page = 1)
        {
            int pageNumber = page;
            int pageSize = 3;
            var lstGiay = db.Giays.GroupBy(x => x.TenGiay)
                        .Select(group => group.First())
                        .ToList();
            var lstShoesDTO = GetListProduct(lstGiay);

            var temp = lstShoesDTO;

            if (occasionCheckboxes.Count > 0)
            {
                lstShoesDTO = lstShoesDTO.Where(x => occasionCheckboxes.Any(occasion => x.TenLoai.Contains(occasion))).ToList();
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                lstShoesDTO = lstShoesDTO.Where(x => x.TenGiay.ToLower().Contains(keyword.ToLower())).ToList();
            }
            if (startPrice.HasValue)
            {
                lstShoesDTO = lstShoesDTO.Where(x => x.GiaBan >= startPrice).ToList();
            }
            if (limitPrice.HasValue)
            {
                lstShoesDTO = lstShoesDTO.Where(x => x.GiaBan <= limitPrice).ToList();
            }
            
            ViewBag.pageSize = pageSize;
            ViewBag.keyword = keyword;
            ViewBag.startPrice = startPrice;
            ViewBag.limitPrice = limitPrice;
            ViewBag.occasionCheckboxes = occasionCheckboxes;

            PagedList<GiayDTO> lst = new PagedList<GiayDTO>(lstShoesDTO, pageNumber, pageSize);
            return PartialView("_ShoesFiltered", lst);
        }

        private List<GiayDTO> GetListProduct(List<Giay> lstShoes)
        {
            List<GiayDTO> lstShoesDTO = new List<GiayDTO>();

            foreach (var item in lstShoes)
            {
                GiayDTO temp = GiayToGiayDTO(item);
                lstShoesDTO.Add(temp);
            }
            return lstShoesDTO;
        }

        public IActionResult Single(string tenGiay)
        {
            var giays = db.Giays.Where(x=>x.TenGiay == tenGiay).OrderBy(x=>x.MaGiay);
            if (giays == null)
            {
                return RedirectToAction("Index");
            }

            List<GiayDTO> lstShoesDTO = new List<GiayDTO>();
            foreach (var item in giays)
            {
                GiayDTO temp = GiayToGiayDTO(item);
                lstShoesDTO.Add(temp);
            }

            return View(lstShoesDTO);
        }

        [HttpGet]
        public ActionResult GetShoesByID(string maGiay)
        {
            return Json(db.Giays.Find(maGiay));
        }

        private GiayDTO GiayToGiayDTO(Giay giay)
        {
            GiayDTO temp = new GiayDTO();
            using( var context = new Qlbangiaynhom7Context())
            {

                temp.MaGiay = giay.MaGiay;
                temp.TenLoai = context.LoaiGiays.First(x => x.MaLoai == giay.MaLoai).TenLoai;
                temp.TenGiay = giay.TenGiay;
                temp.KichCo = giay.KichCo;
                temp.MauSac = giay.MauSac;
                temp.GiaGoc = giay.GiaGoc;
                temp.GiaBan = giay.GiaBan;
                temp.PhanTramGiam = giay.PhanTramGiam;
                temp.DanhGia = giay.DanhGia;
                temp.AnhDaiDien = giay.AnhDaiDien;
                temp.SoLuong = giay.SoLuong;
            }
            return temp;
        }


        public IActionResult Cart()
        {
            return View();
        }

        [Authentication]
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

        [HttpPost]
        public IActionResult AddToCart(string maGioHang, string maGiay, int soLuong = 1)
        {
            try
            {
                if(maGioHang == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }

                var item = db.ChiTietGioHangs.FirstOrDefault(x => x.MaGioHang == maGioHang && x.MaGiay == maGiay);

                if (item != null)
                {
                    item.SoLuong += soLuong;
                    db.ChiTietGioHangs.Update(item);
                }
                else
                {
                    ChiTietGioHang chiTietGioHang = new ChiTietGioHang()
                    {
                        MaGioHang = maGioHang,
                        MaGiay = maGiay,
                        SoLuong = soLuong
                    };
                    db.ChiTietGioHangs.Add(chiTietGioHang);
                }
                UserContext.SoSanPham += soLuong;
                db.SaveChanges();
                return Ok();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
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