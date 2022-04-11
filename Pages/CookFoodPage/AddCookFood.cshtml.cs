using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZeroHunger.Model;
using ZeroHunger.Data;

namespace ZeroHunger.Pages.CookFoodPage
{
    public class AddCookFoodModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public AddCookFoodModel(ApplicationDbContext db)
        {
            _db = db;  
        }
        [BindProperty]
        public CookedFoodDonation cook { get; set; }
        [BindProperty]
        public User donor { get; set; }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            cook.DonorId= (_db.User.Where(b => b.UserEmail.Equals(@User.Identity.Name)).FirstOrDefault());
            if (cook.DonorId == null)
            {
                //ViewData["Message"] = string.Format("Please login first.");
                return RedirectToPage("../login");
            }
            else
            {
                cook.DonorUserID = cook.DonorId.UserID;
                cook.Reservation = 0;
                cook.RemainQuantity = cook.CookQuantity;

                //if (ModelState.IsValid)
                //{
                await _db.CookedFoodDonation.AddAsync(cook);
                await _db.SaveChangesAsync();
                return RedirectToPage("CookFoodView");
            }
            
            //}
            //else
            //{

            //    return RedirectToPage("../Index");
            //}
        }
        
    }
}
