using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.CookFoodPage
{
    public class DonationApplierModel : PageModel
    {
        private ApplicationDbContext _db;
        public List<CookReservation> CR { get; set; }
        public DonationApplierModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task OnGet()
        {
            int uid;
            
            int cid;
            int.TryParse(Request.Query["cookFood"], out cid);
            string uids=HttpContext.Request.Cookies["userid"];
            int.TryParse(uids, out uid);
            CR = await _db.CookReservation.Where(b => b.cookId.Equals(cid)).ToListAsync();
            
            int c = CR.Count;
            if(c!=0)
             { 
            foreach (var item in CR)
            {
                item.reservationRefCook = await _db.CookedFoodDonation.FindAsync(item.cookId);
                if (item.reservationRefCook.DonorUserID!= uid)
                {
                        Response.Redirect("CookFoodView");
                }
                
            }
            foreach (var item in CR)
            {
                item.reservationRefUser = await _db.User.FindAsync(item.userId);
            }
            }



        }
        public async Task<IActionResult> OnPostConfirm(int id)
        {
            CookReservation c= await _db.CookReservation.FindAsync(id);
            
            if (c == null)
            {
                return NotFound();
            }
            CookedFoodDonation cfd = await _db.CookedFoodDonation.FindAsync(c.cookId);
            cfd.RemainQuantity = cfd.RemainQuantity - 1;
            c.status = "Confirmed";
            await _db.SaveChangesAsync();
            return RedirectToPage("CookFoodView");
        }
    }
}
