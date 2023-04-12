using Microsoft.AspNetCore.Mvc;
using ShoesStore.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoesStore.Models.ModelDTOs;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using ShoesStore.Models.ProcedureModels;

namespace ShoesStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    //[Route("")]
    public class AdminController : Controller
    {
        Qlbangiaynhom7Context db = new Qlbangiaynhom7Context();
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }
		//[Route("Index1")]
		//public IActionResult Index1()
		//{
		//	// double proc = Convert.ToDouble(db.ProcTienBan30Ngays.FromSql($"Exec TongTienBan30Ngay").FirstOrDefault());
		//	var proc = db.ProcTienBan30Ngays.FromSql($"Exec TongTienBan30Ngay").ToList().ElementAt(0);
  //          return View(proc);
		//}
		[Route("")]
        [Route("Index")]
		public IActionResult Index()
		{
            AdminContext.SoTienBan30NgayGanNhat = db.ProcTienBan30Ngays.FromSql($"Exec TongTienBan30Ngay").ToList().ElementAt(0);

            int year = 2023;
            // db.ProcTongTienBanHangThangs.FromSql($"EXEC TongTienBanHangThang {year}").ToList();
            AdminContext.SoTienBanTrongNam = db.Set<ProcTongTienBanHangThang>()
                                                    .FromSqlRaw("EXEC TongTienBanHangThang {0}", year)
                                                    .ToList();


            var lstNhanVien = db.NhanViens.OrderBy(x => x.MaNv).ToList();
			return View(lstNhanVien);
		}
		//public IActionResult Index()
  //      {
  //          //var lstNhanVien = db.NhanViens.OrderBy(x => x.MaNv).ToList();
  //          //         double proc = Convert.ToDouble(db.ProcTienBan30Ngays.FromSql($"Exec TongTienBan30Ngay").FirstOrDefault());           
  //          //         var viewModelProc = new ViewModelProc() {
  //          //             lstnhanVien = lstNhanVien,
  //          //             procTiens = proc
  //          //         };
  //          //         return View(viewModelProc);
  //          return View();
  //      }
		[Route("Products")]
        public IActionResult Products()
        {
            var lstGiay = db.Giays.OrderBy(x => x.MaGiay).ToList();
            //var lstGiay = db.Giays.GroupBy(x => x.TenGiay)
                        //.Select(group => group.First())
                        //.ToList();
            return View(lstGiay);
        }
        [Route("TypeProducts")]
        public IActionResult TypeProducts()
        {
            var lstTP = db.LoaiGiays.OrderBy(x => x.MaLoai).ToList();
            return View(lstTP);
        }
        [Route("HDN")]
        public IActionResult HDN()
        {
            var lstHDN = db.HoaDonNhaps.OrderBy(x => x.MaHdn).ToList();
            return View(lstHDN);
        }
        [Route("HDB")]
        public IActionResult HDB()
        {
            var lstHDB = db.HoaDonBans.OrderBy(x => x.MaHdb).ToList();
            return View(lstHDB);
        }      
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }
        [Route("Password")]
        public IActionResult Password()
        {
            return View();
        }
        [Route("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("Error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
