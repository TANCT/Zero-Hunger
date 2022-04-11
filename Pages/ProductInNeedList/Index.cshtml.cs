using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.ProductInNeedList
{
    
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<ProductInNeed> products { get; set; }
        public async Task OnGet()
        {
            products = await _db.ProductInNeed.ToListAsync();
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var product = await _db.ProductInNeed.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _db.ProductInNeed.Remove(product);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }

}
