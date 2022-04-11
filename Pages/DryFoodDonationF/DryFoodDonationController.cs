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

namespace ZeroHunger.Pages.DryFoodDonationF
{
    [Route("api/DryFoodDonation")]
    [ApiController]
    public class DryFoodDonationController : Controller
    {
        
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        public DryFoodDonationController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
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
            string uids = HttpContext.Session.GetString("userid");
            List<DryFoodDonation> dfdl = await _db.DryFoodDonation.ToListAsync();
            dfdl=dfdl.FindAll(x => x.donorid.ToString().Equals(uids)&&x.DryFoodRemainQuantity!=0);
            return Json(new { data = dfdl });
            

            
        }
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var DryFoodDb = await _db.DryFoodDonation.FirstOrDefaultAsync(u => u.Id== id);
        //    if (DryFoodDb == null)
        //    {
        //        return Json(new { success = false, message = "Error while Deleting" });
        //    }
 
        //    _db.DryFoodDonation.Remove(DryFoodDb);
        //    await _db.SaveChangesAsync();

        //    return Json(new { success = true, message = "Delete successful" });
        //}
        public async Task<IActionResult> OnGetDelete(int id)
        {
            
            string uids = HttpContext.Session.GetString("userid");
            if (uids == null)
            {
                return RedirectToPage("../login");
            }
            var DFDs = await _db.DryFoodDonation.FindAsync(id);
            if (DFDs == null)
            {
                return NotFound();
            }
            _db.DryFoodDonation.Remove(DFDs);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
