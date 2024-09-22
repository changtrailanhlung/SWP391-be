using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bos.Model
{
    [Table("AdoptionRegistrationForm")]
    public class AdoptionRegistrationForm 
    {
        [Key]
        public int AdopteID { get; set; }
        [Required]  
        public string IdentityProof { get; set; }
        [Required]  
        public decimal IncomeAmount { get; set; }
        public string? Image { get; set; }
        [Required]  
        public string Condition { get; set; }

        [ForeignKey("User")]
        public int AdopterId { get; set; }

        [ForeignKey("User")]
        public int ShelterStaffId { get; set; }
        [ForeignKey("Pet")]
        public int PetId { get; set; }

        public virtual User? User { get; set; }
        public virtual Pet? Pet { get; set; }

    }
}
