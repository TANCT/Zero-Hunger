using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ZeroHunger.Pages
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnPostAsync()
        {
            await HttpContext.SignOutAsync("ZeroHungerCookie");
            if(Request.Cookies["role"]!=null)
            {
                Response.Cookies.Delete("role");
                Response.Cookies.Delete("userid");
            }
            return RedirectToPage("/login");
        }
    }
}
