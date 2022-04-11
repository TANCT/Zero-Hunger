using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.CookFoodPage
{
    public class EditCookFoodModel : PageModel
    {
    
        private readonly ApplicationDbContext _db;
        public EditCookFoodModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public CookedFoodDonation cookfood { get; set; }
        public async Task OnGet(int id)
        {
            cookfood = await _db.CookedFoodDonation.FindAsync(id);

        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var cookfoodFromDB = await _db.CookedFoodDonation.FindAsync(cookfood.CookID);
                cookfoodFromDB.CookName = cookfood.CookName;
                cookfoodFromDB.CookQuantity = cookfood.CookQuantity;
                cookfoodFromDB.CookLongtitude = cookfood.CookLongtitude;
                cookfoodFromDB.CookLatitude = cookfood.CookLatitude;
                cookfoodFromDB.OpenDate = cookfood.OpenDate;
                cookfoodFromDB.CloseDate = cookfood.CloseDate;
                cookfoodFromDB.Reservation=cookfood.Reservation;
                cookfoodFromDB.RemainQuantity = cookfood.RemainQuantity;

                await _db.SaveChangesAsync();
                return RedirectToPage("CookFoodView");
            }
            else
            {
                return Page();
            }
        }
    }
}
