using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ZeroHunger.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ZeroHunger.Model;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;

namespace ZeroHunger.Controllers
{
    [Route("api/ProductInNeed")]
    [ApiController]
    public class ProductInNeedController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductInNeedController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            this._hostEnvironment = hostEnvironment;
        }

        public ActionResult Index()
        {
            return View("~/Views/Image/Add.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.ProductInNeed.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var pidFromDb = await _db.ProductInNeed.FirstOrDefaultAsync(u => u.product_id == id);
            if (pidFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            if (pidFromDb.image != null)
            {
                string filepath = Path.Combine(_hostEnvironment.WebRootPath, "images", pidFromDb.image);
                if(System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }
                
            }
            _db.ProductInNeed.Remove(pidFromDb);
            await _db.SaveChangesAsync();
            
            return Json(new { success = true, message = "Delete successful" });
        }
        
    }
}
