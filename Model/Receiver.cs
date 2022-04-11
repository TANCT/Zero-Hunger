using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeroHunger.Model
{
    public class Receiver
    {
        [Key][MaxLength(12)]
        public string receiverIC { get; set; }
        [Required]
        public string receiverName { get; set; }
        [Required]
        public char receiverGender { get; set; }
        [Required]
        public string receiverDOB { get; set; }
        [Required]
        public string receiverMarital { get; set; }
        [Required]
        public string receiverOccupation { get; set; }

        public string receiverCompany { get; set; }
        public int receiverSalaryGroupID { get; set; }
        [ForeignKey("receiverSalaryGroupID")][Required]
        public virtual SalaryGroup receiverSalaryGroup { get; set; }
        
        [Required]
        public int receiverFamilyNo { get; set; }
        [Required]
        public string receiverEmail { get; set; }
        [Required]
        public string receiverPhone { get; set; }
        [Required]
        public string receiverAdrs1 { get; set; }
        [Required]
        public string receiverAdrs2 { get; set; }
        public int applicationStatusID { get; set; } = 0;

        public string healthStatus { get; set; }
        public string unhealthyHabit { get; set; }

        public int? userID { get; set; }
        [ForeignKey("userID")]
        public virtual User? user { get; set; }
        

      
    }
}
