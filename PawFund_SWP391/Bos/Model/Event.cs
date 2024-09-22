using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bos.Model
{
    public class Event : BaseEntity
    {
        public int ShelterId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int VolunteerId { get; set; }

        public virtual User? User { get; set; }
        public virtual Shelter? Shelter { get; set; }
    }
}
