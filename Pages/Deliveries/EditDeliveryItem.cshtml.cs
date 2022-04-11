using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.Deliveries
{
    public class EditDeliveryItemModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public int DeliveryID { get; set; }
        [BindProperty]
        public int Quantity { get; set; }
        [BindProperty]
        public DeliveryItem OriDeliveryItem { set; get; }
        [BindProperty]
        public DeliveryItem DeliveryItem { set; get; }
        [BindProperty]
        public Delivery Delivery { set; get; }
        public SelectList DeliveryItemList { set; get; }

        public EditDeliveryItemModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task OnGetAsync(int itemid)
        {
            OriDeliveryItem = _db.DeliveryItem.Find(itemid);
            DeliveryItem = _db.DeliveryItem.Find(itemid);
            var deliveryitems = await _db.DryFoodDonation.Where(d => d.DryFoodRemainQuantity > 0).ToListAsync();
            DeliveryItemList = new SelectList(deliveryitems, "Id", "DryFoodName");
        }
        public async Task<IActionResult> OnPost()
        {
            DeliveryItem DeliveryItemFromDB = await _db.DeliveryItem.FindAsync(OriDeliveryItem.ItemID);//get original delivery item from db
            if (ModelState.IsValid)
            {
                if (DeliveryItemFromDB.DryFoodID != DeliveryItem.DryFoodID)//if dry food changed
                {
                    DryFoodDonation OriDryFood = await _db.DryFoodDonation.FindAsync(DeliveryItemFromDB.DryFoodID);//get original dry food
                    OriDryFood.DryFoodRemainQuantity += DeliveryItemFromDB.Quantity;//add back quantity
                    DeliveryItemFromDB.DryFoodID = DeliveryItem.DryFoodID;//change to new dry food
                    DeliveryItemFromDB.Quantity = DeliveryItem.Quantity;//set new quantity
                }
                else//if dry food not changed
                {             
                    DryFoodDonation DryFoodFromDB = await _db.DryFoodDonation.FindAsync(DeliveryItem.DryFoodID);//get dry food
                    DryFoodFromDB.DryFoodRemainQuantity += DeliveryItemFromDB.Quantity;//add back quantity
                    DeliveryItemFromDB.Quantity = DeliveryItem.Quantity;    //set new quantity
                }   
                var newdryfood = _db.DryFoodDonation.Find(DeliveryItem.DryFoodID);  //find latest dry food which is the delivery item
                newdryfood.DryFoodRemainQuantity -= DeliveryItem.Quantity;  //minus the dry food with new quantity
                await _db.SaveChangesAsync();
                return RedirectToPage("DeliveryItem", new { id = DeliveryItemFromDB.DeliveryID });
            }
            return Page();
        }
    }
}
