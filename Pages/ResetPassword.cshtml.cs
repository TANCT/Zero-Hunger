
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ZeroHunger.Pages
{
    public class ResetPasswordModel : PageModel
    {
        [BindProperty]
        public string password{ get; set; } = "";
        public int id { get; set; }
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public User forgotUser { get; set; }
        public ResetPasswordModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task OnGetAsync(string p,string n, string a)
        {
            id = int.Parse(p)+87-777;
            forgotUser = await _db.User.FindAsync(id);
        }

        public async Task<IActionResult> OnPostAsync(string password, int id)
        {
            //forgotUser = await _db.User.FindAsync(forgotUser.UserID);
            User resetPwdUser = _db.User.Where(b => b.UserID.Equals(id)).FirstOrDefault();
            if (resetPwdUser != null)
            {
                resetPwdUser.UserPwd = password;
                await _db.SaveChangesAsync();
                if (@User.Identity.IsAuthenticated)
                {
                    ViewData["Message"] = string.Format("You password was updated.");

                    return RedirectToPage("Index");
                }
                else
                {
                    ViewData["Message"] = string.Format("You password was updated.\\Please try to login.");

                    return RedirectToPage("login");
                }
            }
            else
            {
                ViewData["Message"] = string.Format("Error occured.\\Please try again.");
                return Page();
            }
        }
    }
}
