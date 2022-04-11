using ZeroHunger.Data;
using ZeroHunger.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;


namespace ZeroHunger.Pages.Deliveries
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Delivery Delivery { set; get; }
        public SelectList VolunteerList { get; set; }
        public SelectList ReceiverList { get; set; }
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task OnGet(int id)
        {
            var items = await _db.User.Where(u => u.UserType.TypeID == 2).ToListAsync();
            VolunteerList = new SelectList(items, "UserID", "UserName");
            var bitems = await _db.User.Where(u => u.UserType.TypeID == 1).ToListAsync(); ;
            ReceiverList = new SelectList(bitems, "UserID", "UserName");

            Delivery = _db.Delivery.Find(id);
        }
        public async Task<IActionResult> OnPost()
        {            
            _db.Delivery.Update(Delivery);
            await _db.SaveChangesAsync();              
            return RedirectToPage("Index");     
        }
    }
}
