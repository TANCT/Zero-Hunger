using ZeroHunger.Data;
using ZeroHunger.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ZeroHunger.Pages.Deliveries
{
    public class VolunteerDeliveryModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IEnumerable<Delivery> MyDeliveries { set; get; }
        [BindProperty]
        public Delivery Delivery { set; get; }
        public VolunteerDeliveryModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }
    }
}
