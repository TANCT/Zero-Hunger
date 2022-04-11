using ZeroHunger.Data;
using ZeroHunger.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZeroHunger.Pages.Deliveries
{
    
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Delivery Delivery { set; get; }

        public SelectList VolunteerList { get; set; }

        public SelectList ReceiverList { get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task OnGetAsync()
        {
            var items = await _db.User.Where(u => u.UserType.TypeID == 2).ToListAsync();
            VolunteerList = new SelectList(items, "UserID", "UserName");
            
            var ritems = await _db.User.Where(u => u.UserType.TypeID == 1).ToListAsync();
            ReceiverList = new SelectList(ritems, "UserID", "UserName");
        }
        public async Task<IActionResult> OnPost()
        {
            if (Delivery.DeliveryStatus != 0)
            {
                ModelState.AddModelError("Delivery.DeliveryStatus", "The delivery status must be pending now.");
            }
            int result = System.DateTime.Compare(Delivery.DeliveryTime, System.DateTime.Now.AddMinutes(30));
            if (result < 0)
            {
                ModelState.AddModelError("Delivery.DeliveryTime", "The delivery time must be at least 30 minutes starting from now within today only.");
            }
            if (!ModelState.IsValid)
            {
                var items = await _db.User.Where(u => u.UserType.TypeID == 2).ToListAsync();
                VolunteerList = new SelectList(items, "UserID", "UserName");
                var ritems = await _db.User.Where(u => u.UserType.TypeID == 1).ToListAsync();
                ReceiverList = new SelectList(ritems, "UserID", "UserName");
            }
            if (ModelState.IsValid)
            {
                await _db.Delivery.AddAsync(Delivery);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return Page();
        }

    }
}
