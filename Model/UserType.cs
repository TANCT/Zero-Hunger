using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ZeroHunger.Model
{
    public class UserType
    {
        [Key]
        public int TypeID { get; set; }
        [Required]
        [MaxLength(255)]
        public string TypeName { get; set; }
        [MaxLength(255)]

        public string TypeInterface { get; set; }
       
        
    }
}
