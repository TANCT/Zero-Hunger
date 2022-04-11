using ZeroHunger.Data;
using ZeroHunger.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections;

namespace ZeroHunger.Pages.Deliveries
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
      
        public IEnumerable<Delivery> Deliveries { set; get; }
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
           
        }
    }
}
