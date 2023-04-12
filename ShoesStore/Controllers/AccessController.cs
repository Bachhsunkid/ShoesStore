using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ShoesStore.Models;
using ShoesStore.Models.ModelDTOs;
using System.Data.Common;

namespace ShoesStore.Controllers
{
	public class AccessController : Controller
	{
        Qlbangiaynhom7Context db = new Qlbangiaynhom7Context();

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("TaiKhoan") == null)
            {
                return View();
            }
            
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Login(Tuser user)
        {
            if (HttpContext.Session.GetString("TaiKhoan") == null)
            {
                var obj = db.Tusers.Where(x => x.TaiKhoan == user.TaiKhoan && x.MatKhau == user.MatKhau).FirstOrDefault();
                if (obj != null)
                {
                    if (obj.Role == 0)
                    {
                        return RedirectToAction("Index", "Admin", new { area = "Admin" });
                    }
                    else
                    {
                        UserContext.IsLogin = true;
                        UserContext.TaiKhoan = obj.TaiKhoan;
                        var query1 = from Tuser in db.Tusers
                                     join KhachHang in db.KhachHangs
                                     on UserContext.TaiKhoan equals KhachHang.TaiKhoan
                                     select KhachHang.MaKh;
                        UserContext.MaKH = query1.ToList().ElementAt(0);

                        var query2 = from KhachHang in db.KhachHangs
                                     join GioHang in db.GioHangs
                                     on UserContext.MaKH equals GioHang.MaKh
                                     select GioHang.MaGioHang;
                        UserContext.MaGioHang = query2.ToList().ElementAt(0);
                        UserContext.SoSanPham = (int)db.ChiTietGioHangs.Where(c => c.MaGioHang == UserContext.MaGioHang).Sum(c => c.SoLuong);
                        return RedirectToAction("Index", "Home");
                    }

                    HttpContext.Session.SetString("TaiKhoan", obj.TaiKhoan.ToString());
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = "Incorrect username or password !";
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(Tuser user)
        {
            if (ModelState.IsValid)
            {
                var check = db.Tusers.FirstOrDefault(s => s.TaiKhoan == user.TaiKhoan);
                if (check == null)
                {
                    //user.Role = 1;
                    //db.Tusers.Add(user);
                    //db.SaveChanges();
                    //var taiKhoan = new SqlParameter("@TaiKhoan", user.TaiKhoan);
                    //var matKhau = new SqlParameter("@MatKhau", user.MatKhau);
                    //db.ExecuteSqlCommand("InsertUser @TaiKhoan, @MatKhau", taiKhoan, matKhau);

                    var nameParam = new SqlParameter("@TaiKhoan", user.TaiKhoan);
                    var emailParam = new SqlParameter("@MatKhau", user.MatKhau);
                    var parameters = new DbParameter[] { nameParam, emailParam };

                    db.Database.ExecuteSqlRaw("EXEC InsertUser @TaiKhoan, @MatKhau", parameters);
                    return RedirectToAction("Login", "Access");
                }
                else
                {
                    TempData["ErrorMessage"] = "Username already exists !";
                    return View();
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("TaiKhoan");
            UserContext.IsLogin = false;
            UserContext.TaiKhoan = "";
            UserContext.MaKH = "";
            UserContext.MaGioHang = "";
            return RedirectToAction("Login", "Access");
        }
    }
}
