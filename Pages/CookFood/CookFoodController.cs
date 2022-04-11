using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.CookFood
{
    [Route("api/CookReservation")]
    [ApiController]
    public class CookFoodController : Controller
    {
        
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        public IEnumerable<CookReservation> CR { get; set; }
        public IEnumerable<CookReservation> CRP { get; set; }
        public CookFoodController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            this._hostEnvironment = hostEnvironment;
        }
        public ActionResult Index()
        {
            return View("Index.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            int uid;
            int.TryParse(HttpContext.Session.GetString("userid"), out uid);
            CR = await _db.CookReservation.Where(b => b.userId.Equals(uid)).ToListAsync();
            foreach (var item in CR)
            {
                item.reservationRefCook = await _db.CookedFoodDonation.FindAsync(item.cookId);
                
            }
            CRP = CR.Where(c => c.status.Equals("Pending"));
            
            CR = CR.Where(c => c.status.Equals("Success") || c.status.Equals("Expired")||c.status.Equals("Confirmed"));
            
            return Json(new { data = CR });
        }
        
       
        
    }
}
