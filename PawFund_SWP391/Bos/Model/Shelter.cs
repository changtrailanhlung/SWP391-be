using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bos.Model
{
    [Table("Shelter")]
    public class Shelter 
    {
        [Key]
        public int ShelterID { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int Capaxity { get; set; }

        public string? Email { get; set; }
        [Required]
        public string Website { get; set; }

        public virtual ICollection<Pet>? Pets { get; set; }
        public virtual ICollection<Event>? Events { get; set; }

        public virtual Donation? Donation { get; set; }
        public virtual ShelterStaff? ShelterStaff { get; set; }



    }
}
