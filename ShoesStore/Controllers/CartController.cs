using Microsoft.AspNetCore.Mvc;
using ShoesStore.Models;
using ShoesStore.Models.ModelDTOs;
using ShoesStore.ViewModels;
using X.PagedList;

namespace ShoesStore.Controllers
{
	public class CartController : Controller
	{
        Qlbangiaynhom7Context db = new Qlbangiaynhom7Context();

        [HttpPost]
        public IActionResult GetChiTietGioHang(int page = 1)
        {
            List<ChiTietGioHangGiayModel> lstShoesInCart = new List<ChiTietGioHangGiayModel>();
            var giaycart = db.ChiTietGioHangs.Where(x => x.MaGioHang == UserContext.MaGioHang).ToList();

            foreach (var item in giaycart)
            {
                lstShoesInCart.Add(new ChiTietGioHangGiayModel()
                {
                    ChiTietGioHang = item,
                    Giay = db.Giays.Find(item.MaGiay)
                });

            }

            return PartialView("_Cart", lstShoesInCart);
        }
    }
}
