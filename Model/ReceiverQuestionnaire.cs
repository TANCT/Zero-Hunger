using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZeroHunger.Model
{
    public class ReceiverQuestionnaire
    {
        [Key]
        public int questionnaireId { get; set; }
        public string receiverIC { get; set; }
        [ForeignKey("receiverIC")]
        [Required]
        public virtual Receiver receiver { get; set; }
        [Required]
        public string date { get; set; }
        [Required]
        public string reason { get; set; }
        [Required]
        public string otherSponsorship { get; set; }
        [Required]
        public string property { get; set; }
        public string additional { get; set; }
    }
}
