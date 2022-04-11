using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ZeroHunger.Pages.DryFoodDonationF
{
    public class HistoryDonationModel : PageModel
    {
        public async Task<IActionResult> OnGet()
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
                    return Page();
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}
