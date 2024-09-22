using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bos.Model
{
    public class Shelter : BaseEntity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public int Capaxity { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

        public virtual ICollection<Pet>? Pets { get; set; }
        public virtual ICollection<Event>? Events { get; set; }

        public virtual Donation? Donation { get; set; }
        public virtual ShelterStaff? ShelterStaff { get; set; }



    }
}
