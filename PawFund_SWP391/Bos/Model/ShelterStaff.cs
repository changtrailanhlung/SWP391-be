using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bos.Model
{
    [Table("ShelterStaff")]
    public class ShelterStaff 
    {
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Shelter")]
        public int ShelterId { get; set; }

        public virtual ICollection<User>? Users { get; set; }
        public virtual Shelter? Shelter { get; set; }
    }
}
