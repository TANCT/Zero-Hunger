using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeroHunger.Model
{
    public class DeliveryItem
    {
        [Key]
        public int ItemID { get; set; }
        [Required]
        public int DryFoodID { get; set; }
        [Required]
        public int DeliveryID { get; set; }
        [Required]
        public int Quantity { get; set; }



    }
}
