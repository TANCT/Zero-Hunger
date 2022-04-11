using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace ZeroHunger.Model
{
    [Table("DryFoodDonation")]
    public class DryFoodDonation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string DryFoodName { get; set; }
        [Required,]
        public int DryFoodQuantity { get; set; }
        public int DryFoodRemainQuantity { get; set; }
        [Required]
        public string DryFoodPickDate { get; set; }
        [Required]
        public string DryFoodRemark { get; set; }
        [Required]
        public string DeliveryMethod { get; set; }
        public int donorid { get; set; }
        public int? deliveryid { get; set; }
        [ForeignKey("DonorIdfk")]
        public virtual User donor_Id { get; set; }
        [ForeignKey("DelivererId")]
        public virtual User deliverer_Id { get; set; }
        



    }


}
