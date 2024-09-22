using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bos.Model
{
    [Table("Certification")]
    public class Certification 
    {
        [Key] public int CertificationID { get; set; }
        public string? Image { get; set; }
        public string? Desciption { get; set; }
        [Required]  
        public DateTime Date { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Pet")]
        public int PetId { get; set; }

        public virtual User? User { get; set; }
        public virtual Pet? Pet { get; set; }
    }
}
}
