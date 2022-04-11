using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.DryFoodDonationF
{
    [Route("api/DFDHistory")]
    [ApiController]
    public class HistoryDonationController : Controller
    {
        private readonly ApplicationDbContext _db;
        
        public HistoryDonationController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            string uids = HttpContext.Session.GetString("userid");
            List<DryFoodDonation> dfdl = await _db.DryFoodDonation.ToListAsync();
            dfdl = dfdl.FindAll(x => x.donorid.ToString().Equals(uids) && x.DryFoodRemainQuantity == 0);
            return Json(new { data = dfdl });



        }
    }
}
