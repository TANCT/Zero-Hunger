using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.DryFoodDonationF
{
    public class EditDryFoodDonationModel : PageModel
    {
        private ApplicationDbContext _db;
        public EditDryFoodDonationModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public DryFoodDonation DFD { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            string uids = HttpContext.Session.GetString("userid");
            if (uids == null)
            {
                return RedirectToPage("../login");
            }
            else
            {
                if (HttpContext.Request.Cookies["role"].Equals("3"))
                {
                    DFD = await _db.DryFoodDonation.FindAsync(id);
                    return Page();
                }
                else
                {
                    return NotFound();
                }
            }
            


        }
        public async Task<IActionResult> OnPost()
        {
            var User = await _db.User.FindAsync(DFD.donorid);
            
            if (ModelState.IsValid)
            {
                DryFoodDonation DFDFromDb= await _db.DryFoodDonation.FindAsync(DFD.Id);
                DFDFromDb.DryFoodName = DFD.DryFoodName;
                DFDFromDb.DryFoodQuantity= DFD.DryFoodQuantity;
                DFDFromDb.DeliveryMethod = DFD.DeliveryMethod;
                DFDFromDb.DryFoodPickDate = DFD.DryFoodPickDate;
                DFDFromDb.DryFoodRemark = DFD.DryFoodRemark;
                await _db.SaveChangesAsync();
                return RedirectToPage("DryFoodIndex");
            }
            else
            {
                return Page();
            }
        }
        
    }
}
