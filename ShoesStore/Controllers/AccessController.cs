using Microsoft.AspNetCore.Mvc;
using ShoesStore.Models;
using ShoesStore.Models.ModelDTOs;

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
                    UserContext.IsLogin = true;
                    HttpContext.Session.SetString("TaiKhoan", obj.TaiKhoan.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public IActionResult SignUp()
        {

                return View();

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("TaiKhoan");
            UserContext.IsLogin = false;
            return RedirectToAction("Login", "Access");
        }
    }
}
