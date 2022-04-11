using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZeroHunger.Model;
using ZeroHunger.Data;

namespace ZeroHunger.Pages.UserPage
{
    public class CreateUserModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public CreateUserModel(ApplicationDbContext db)
        {
            _db = db;  
        }
        [BindProperty]
        public User user { get; set; }
        public string message { get; set; } = "";
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            //userObj.UserID = "Lalisa";
            if (ModelState.IsValid)
            {
                if(_db.User.Where(x => x.UserEmail.Equals(user.UserEmail)) == null)
                {
                    await _db.User.AddAsync(user);
                    await _db.SaveChangesAsync();
                    return RedirectToPage("UserView");
                }
                else
                {
                    message = "This user exists. Please try again.";
                    return Page();
                }
                
            }
            else
            {

                return RedirectToPage("../Index");
            }
        }
    }
}
