using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ZeroHunger.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ZeroHunger.Model;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;


namespace ZeroHunger.Controllers
{
    [Route("api/DeliveryItem")]
    [ApiController]
    public class DeliveryItemController : Controller
    {
        private readonly ApplicationDbContext _db;
      
        public DeliveryItemController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> SetMaxQuantity(int id)
        {
            List<DryFoodDonation> dfdlist = await _db.DryFoodDonation.ToListAsync();
            var dryfood = dfdlist.Where(d => d.Id == id).FirstOrDefault();
            return new JsonResult(dryfood.DryFoodRemainQuantity);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetPhoneAdrs(int id)
        {
            List<User> receiverlist = await _db.User.Where(u => u.UserType.TypeID == 1).ToListAsync();
            var idUser = _db.User.Where(i => i.UserID == id);
            var selected = _db.User.Where(s => receiverlist.Contains((User)idUser));
            var selectreceiver = receiverlist.Where(d => d.UserID.Equals(id)).FirstOrDefault();
            return new JsonResult(selectreceiver);
        }

    }
}