using System.ComponentModel.DataAnnotations;

namespace ZeroHunger.Model
{
    public class SalaryGroup
    {
        [Key]
        public int salaryGroupID { get; set; }
        [Required]
        public string salaryRange { get; set; }
    }
}
