using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages
{
    public class ViewDeliveryModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public ViewDeliveryModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Delivery> deliveryList { get; set; }
        

        public async void OnGet(int id)
        {
            deliveryList = _db.Delivery.OrderByDescending(s => s.DeliveryTime).Where(r => r.ReceiverID == id && r.DeliveryStatus!=DeliveryStatus.Rejected);



        }

    }
}
