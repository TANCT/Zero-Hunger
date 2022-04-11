using ZeroHunger.Data;
using ZeroHunger.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Collections.Generic;

namespace ZeroHunger.Pages.Deliveries
{
    public class ViewDeliveryItemModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IEnumerable<Delivery> MyDeliveries { set; get; }
        public List<DeliveryItem> DeliveryItems { set; get; }
        public IEnumerable<DryFoodDonation> DryFoods { set; get; }
        public DeliveryItem DeliveryItem { set; get; }
        public bool ItemFound { set; get; } = false;
    
        public ViewDeliveryItemModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            DeliveryItems = _db.DeliveryItem.Where(d => d.DeliveryID == id).ToList();
            DryFoods = _db.DryFoodDonation;
        }
    }
}
