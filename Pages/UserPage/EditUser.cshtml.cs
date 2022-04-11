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
    public class EditUserModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public EditUserModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public User user { get; set; }
        public async Task OnGet(int id)
        {
            user = await _db.User.FindAsync(id);

        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var userFromDB = await _db.User.FindAsync(user.UserID);
                userFromDB.UserName = user.UserName;
                userFromDB.UserPwd = user.UserPwd;
                userFromDB.UserEmail = user.UserEmail;
                userFromDB.UserPhone = user.UserPhone;
                userFromDB.UserBirth = user.UserBirth;
                userFromDB.UserAdrs1 = user.UserAdrs1;
                userFromDB.UserAdrs2 = user.UserAdrs2;
                await _db.SaveChangesAsync();
                return RedirectToPage("UserView");
            }
            else
            {
                return Page();
            }
        }
    }
}
