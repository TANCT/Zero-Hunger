using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ZeroHunger.Model;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace ZeroHunger.Model
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public virtual UserType UserType{ get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserPwd { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string UserPhone { get; set; }
        public DateTime UserBirth { get; set; }
        public string UserAdrs1 { get; set; }
        public string UserAdrs2 { get; set; }
        public float? latitude { get; set; }
        public float? longitute { get; set; }
        [DisplayName("Image")]
        public string ProfileImage { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public bool RememberMe { get; set; }
        [InverseProperty("Volunteer")]
        public ICollection<Delivery> VolunteerDeliveries { get; set; }
        [InverseProperty("Receiver")]
        public ICollection<Delivery> ReceiverDeliveries { get; set; }
    }
}
