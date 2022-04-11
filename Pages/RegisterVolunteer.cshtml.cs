using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;
using ZeroHunger.Model;
using ZeroHunger.Data;

namespace ZeroHunger.Pages
{
    public class RegisterVolunteerModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public RegisterVolunteerModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public User user { get; set; }
        public string message { get; set; } = "";
        public string fName { get; set; }
        public string lName { get; set; }
        public string zip { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string add1 { get; set; }
        public string add2 { get; set; }
        public string code { get; set; }
        public string phone { get; set; }
        public string transport { get; set; }
        [BindProperty]
        public UserType userType { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(string fName, string lName, string add1, string add2, string zip, string city, string state, string code, string phone)
        {

            var obj = _db.User.Where(a => a.UserEmail.Equals(user.UserEmail)).FirstOrDefault();
            if (obj == null)
            {
                
                user.UserType = (UserType)(_db.UserType.Where(b => b.TypeID.Equals(2)).FirstOrDefault());
                user.UserName = fName + " " + lName;
                user.UserAdrs1 = add1;
                if (add2 != null)
                {
                    user.UserAdrs1 += ", " + add2;
                }
                user.UserAdrs2 = zip + " " + city + ", " + state;
                user.UserPhone = code + phone;
                //user.DonorType= type;

                await _db.User.AddAsync(user);
                await _db.SaveChangesAsync();
                return RedirectToPage("Login");
            }
            else
            {
                ViewData["Message"] = string.Format("This email was registered. Please login instead.");
                return Page();
            }



        }
    }
}
