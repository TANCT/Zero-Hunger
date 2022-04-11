using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeroHunger.Model
{
    public class ReceiverFamily
    {
        [Key]
        [MaxLength(12)]
        public string familyIC { get; set; }
        public string receiverIC { get; set; }
        [ForeignKey("receiverIC")] [Required]
        public virtual Receiver receiver { get; set; }
        [Required]
        public string familyName { get; set; }
        [Required]
        public string relationship { get; set; }
        public string familyCompanyOrSchool { get; set; }

        [Required]
        public string familyDOB { get; set; }
        [Required]
        public string familyOccupation { get; set; }
        public int familySalaryGroupID { get; set; }
        [ForeignKey("familySalaryGroupID")][Required]
        public virtual SalaryGroup familySalaryGroup { get; set; }
        [Required]
        public string healthStatus { get; set; }
        [Required]
        public string unhealthyHabit { get; set; }
        
        
    }
}
