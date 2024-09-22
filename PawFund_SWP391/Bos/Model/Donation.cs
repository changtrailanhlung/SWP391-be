using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bos.Model
{
    [Table("Donation")]
    public class Donation
    {
        [Key] public int DonationID { get; set; }
        [Required]  
        public string Amount { get; set; }
        [Required]  
        public DateTime Date { get; set; }

        [ForeignKey("User")]
        public int DonorId { get; set; }

        [ForeignKey("Shelter")]
        public int ShelterId { get; set; }

        public virtual User? User { get; set; }
        public virtual Shelter? Shelter { get; set; }
    }
}
