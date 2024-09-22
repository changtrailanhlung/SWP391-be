using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bos.Model
{
    [Table("Event")]
    public class Event 
    {
        [Key] public int EventID { get; set; }

        [ForeignKey("Shelter")]
        public int ShelterId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        [Required]  
        public string Location { get; set; }

        [ForeignKey("User")]
        public int VolunteerId { get; set; }

        public virtual User? User { get; set; }
        public virtual Shelter? Shelter { get; set; }
    }
}
