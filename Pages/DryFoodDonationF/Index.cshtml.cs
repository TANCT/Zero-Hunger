using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.DryFoodDonationF
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<DryFoodDonation> DFD { get; set; }
        public async Task<IActionResult> OnGet()
        {
            DFD= await _db.DryFoodDonation.Where(b=>b.DryFoodRemainQuantity!=0).ToListAsync();
            
            string uids = HttpContext.Session.GetString("userid");
            if (uids == null)
            {
                return RedirectToPage("../login");
            }
            else
            {
                if (HttpContext.Request.Cookies["role"].Equals("3"))
                {
                    return Page();
                }
                else
                {
                    return NotFound();
                }
            }
        }
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
