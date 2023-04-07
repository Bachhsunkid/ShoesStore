using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoesStore.Models;

namespace ShoesStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeAPIController : ControllerBase
    {
        Qlbangiaynhom7Context db = new Qlbangiaynhom7Context();

        [HttpGet]
        public IEnumerable<Giay> GetAllShoes()
        {
            var lstGiay = db.Giays.Select(x => x.TenGiay).Distinct().ToList();
            return (IEnumerable<Giay>)lstGiay;
        }
    }
}
