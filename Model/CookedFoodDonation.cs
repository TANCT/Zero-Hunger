using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ZeroHunger.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeroHunger.Model
{
    public class CookedFoodDonation
    {
        [Key]
        public int CookID { get; set; }
        [Required]
        public string CookName { get; set; }
        [Required]
        public int CookQuantity { get; set; }
        [Required]
        public int Reservation { get; set; }
        [Required]
        public int RemainQuantity { get; set; }
        [Required]
        public string ShopName { get; set; }
        [Required]
        public DateTime OpenDate { get; set; }
        [Required]
        public DateTime CloseDate { get; set; }

        [Required]
        public float CookLongtitude { get; set; }
        [Required]
        public float CookLatitude { get; set; }
        public int DonorUserID { get; set; }
        [ForeignKey("DonorUserID"),Required]
        public virtual User DonorId { get; set; }
        [NotMapped]
        public float distance { get; set; }
    }
}
