using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZeroHunger.Model;
using System;
using System.Collections.Generic;

namespace ZeroHunger.Model
{
    public class Delivery
    {
        [Key]
        [Display(Name = "Delivery ID")]
        public int DeliveryID { get; set; }
        [Required]
        [Display(Name = "Delivery Status")]
        public DeliveryStatus DeliveryStatus { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Delivery Time")]
        public DateTime DeliveryTime { get; set; }
        public int? VolunteerID { get; set; }
        public int ReceiverID { get; set; }
        [ForeignKey("Volunteerref")]
        public virtual User? Volunteer { get; set; }
        [ForeignKey("Receiverref")]
        public virtual User Receiver { get; set; }
        
    }
}
