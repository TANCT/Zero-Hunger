using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeroHunger.Model
{
    public class ProductInNeed
    {
        [Key]
        public int product_id { get; set; }
        [Required]
        public string product_name { get; set; }
        public string product_description { get; set; }
        [Required]
        public int amount { get; set; }
       
        
        [DisplayName("Image")]
        public string image { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }





    }
}
