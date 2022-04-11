using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages
{
    public class profileModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public profileModel(ApplicationDbContext db)
        {
            _db = db;

        }

        [BindProperty]
        public IEnumerable<User> Users { get; set; }
        public User loginUser { get; set; }
        public string name { get; set; } = "";
        public string email { get; set; } = "";
        public string address1 { get; set; } = "";
        public string address2 { get; set; } = "";
        public string birth { get; set; }
        public string phone{ get; set; } = "";
        public int p { get; set; }
        public string n { get; set; } = "";
        public string a { get; set; } = "";
        public User u { get; set; }

        private Random _random = new Random();

        public string RandomString(int size, bool lowerCase = true)
        {
            var builder = new StringBuilder(size);
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
        public void OnGet()
        {
            
            loginUser = _db.User.Where(b => b.UserEmail.Equals(@User.Identity.Name)).FirstOrDefault();
            u = _db.User.Where(b => b.UserEmail.Equals(@User.Identity.Name)).FirstOrDefault();
            name = loginUser.UserName;
            email = loginUser.UserEmail;
            address1 = loginUser.UserAdrs1;
            address2 = loginUser.UserAdrs2;
            birth = loginUser.UserBirth.ToString("dd/MM/yyyy");
            phone = loginUser.UserPhone;
            p = (loginUser.UserID +777) - 87;
            n = RandomString(20,true);
            a = RandomString(20, true);
        }

        public async Task<IActionResult> OnPostUpdate(string userLat, string userLon, string handler)
        {
            string userlat = userLat;
            string userlon = userLon;
            string uids = HttpContext.Session.GetString("userid");
            int uid;
            int.TryParse(uids, out uid);
            u = await _db.User.FindAsync(uid);
            float userlatitude;
            float userlongitute;
            float.TryParse(userlat, out userlatitude);
            float.TryParse(userlon, out userlongitute);
            u.latitude = userlatitude;
            u.longitute = userlongitute;
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
