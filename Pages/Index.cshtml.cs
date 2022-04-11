using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeroHunger.Model;
using ZeroHunger.Data;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace ZeroHunger.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly ApplicationDbContext _db;

        public IEnumerable<ProductInNeed> products { get; set; }

        public IndexModel(ApplicationDbContext db)
        {
            //_logger = logger;
            _db = db;
        }

        public async Task OnGet()
        {
            if((Request.Cookies["ZeroHungerCookie"]!=null))
                {
                User u = _db.User.Where(b => b.UserEmail.Equals(@User.Identity.Name)).FirstOrDefault();
                if(u!=null)
                {
                if (HttpContext.Session.GetString("userid") == null)
                {
                    HttpContext.Session.SetString("userid", u.UserID.ToString());
                }
                }
            }
            if (!User.Identity.IsAuthenticated)
            {
                if (Request.Cookies["role"] != null)
                {
                    Response.Cookies.Delete("role");
                    Response.Cookies.Delete("userid");
                    Response.Redirect("Index");
                }

            }
            //
            //HttpContext.Session.setString("userid", User.Identity.Name);
            products = await _db.ProductInNeed.ToListAsync();
        }
    }
}
