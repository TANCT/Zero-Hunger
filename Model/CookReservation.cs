using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZeroHunger.Model
{
    public class CookReservation
    {
        [Key]
        public int Id { get; set; }
        
        public int cookId { get; set; }
        
        public int userId { get; set; }
        [Required]
        public string date { get; set; }
        public string status { get; set; }
        [ForeignKey("cookId"),Required]
        public virtual CookedFoodDonation reservationRefCook{ get; set; }
        [ForeignKey("userId")]
        public virtual User reservationRefUser { get; set; }

    }
}
