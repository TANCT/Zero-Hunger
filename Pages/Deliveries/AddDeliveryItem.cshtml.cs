using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.Deliveries
{
    public class AddDeliveryItemModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public int DeliveryId { get; set; }
        [BindProperty]
        public DeliveryItem DeliveryItem { set; get; }
        public Delivery Delivery { set; get; }
        public SelectList DeliveryItemList { set; get; }

        public AddDeliveryItemModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task OnGet(int id)
        {
            DeliveryId = id;
            Delivery = _db.Delivery.Find(id);
            var deliveryitems = await _db.DryFoodDonation.Where(d => d.DryFoodRemainQuantity > 0).ToListAsync();
            DeliveryItemList = new SelectList(deliveryitems, "Id", "DryFoodName");
        }

        public async Task<IActionResult> OnPost()
        {
            
            var item = await _db.DryFoodDonation.Where(d => d.Id == DeliveryItem.DryFoodID).FirstOrDefaultAsync();
            var max = item.DryFoodRemainQuantity;
            if (DeliveryItem.Quantity < 0 || DeliveryItem.Quantity > max)
            { 
                  ModelState.AddModelError("DeliveryItem.Quantity", "The quantity of the delivery item must not be lesser than 1. It must be between 1 and " + max + ".");
            }
            if (DeliveryItem.Quantity > max)
            {
                ModelState.AddModelError("DeliveryItem.Quantity", "The quantity of the delivery item should not be greater than " + max +". It must be between 1 and " + max + ".");
            }

            if (ModelState.IsValid)
            {
                await _db.DeliveryItem.AddAsync(DeliveryItem);
                var dryfood = _db.DryFoodDonation.Find(DeliveryItem.DryFoodID);
                dryfood.DryFoodRemainQuantity -= DeliveryItem.Quantity;
                await _db.SaveChangesAsync();
                return RedirectToPage("DeliveryItem", new { id = DeliveryItem.DeliveryID });
            }
            return Page();
        }
    }
}
