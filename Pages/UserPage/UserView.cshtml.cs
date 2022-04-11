using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using ZeroHunger.Model;
using Microsoft.AspNetCore.Authorization;
using ZeroHunger.Data;

namespace ZeroHunger.Pages.UserPage
{
   // [Authorize(Policy = "MustBelongToAdmin")]
    public class UserViewModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public UserViewModel(ApplicationDbContext db)
        {
            _db = db;

        }
        [BindProperty]
        public IEnumerable<User> Users { get; set; }
        public UserType UserType { get; set; }
        public async Task OnGet()
        {
            Users = await _db.User.ToListAsync();
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var user = await _db.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _db.User.Remove(user);
            await _db.SaveChangesAsync();
            return RedirectToPage("UserView");
        }
    }
}
