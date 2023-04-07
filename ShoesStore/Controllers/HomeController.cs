using Microsoft.AspNetCore.Mvc;
using ShoesStore.Models;
using System.Diagnostics;

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
            return View();
        }

        public IActionResult Shop()
        {
            var lstGiay = db.Giays.GroupBy(x => x.TenGiay)
                        .Select(group => group.First())
                        .ToList();
            return View(lstGiay);
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