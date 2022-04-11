using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using ZeroHunger.Model;
using ZeroHunger.Data;

namespace ZeroHunger.Pages.CookFoodPage
{
    public class CookFoodViewModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CookFoodViewModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<CookedFoodDonation> cookfood { get; set; }
        public async Task OnGet()
        {
            cookfood = await _db.CookedFoodDonation.ToListAsync();
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var cook= await _db.CookedFoodDonation.FindAsync(id);
            if (cook == null)
            {
                return NotFound();
            }
            _db.CookedFoodDonation.Remove(cook);
            await _db.SaveChangesAsync();

            return RedirectToPage("CookFoodView");
        }
        
    }
}
