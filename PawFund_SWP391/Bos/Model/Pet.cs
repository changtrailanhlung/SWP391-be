using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bos.Model
{
    [Table("Pet")]
    public class Pet 
    {
        [Key] public int PetID { get; set; }
        [ForeignKey("Shelter")]
        public int ShelterID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        [Required]  
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]  
        public string Breed { get; set; }
        public string? Gender { get; set; }
        public int? Age { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
        [Required]
        public string AdoptionStatus { get; set; }

        // Foreign key for Status
        [ForeignKey("Status")]
        public int StatusId { get; set; }

        public string? Image { get; set; }

        // Navigation properties
        public virtual User? User { get; set; }
        public virtual AdoptionRegistrationForm? AdoptionRegistrationForm { get; set; }
        public virtual Certification? Certification { get; set; }
        public virtual Shelter? Shelter { get; set; }

        // Reference to Status
        public virtual Status? Status { get; set; }
    }
}
