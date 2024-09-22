using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bos.Model
{
    public class Donation : BaseEntity
    {
        public string Amount { get; set; }
        public DateTime Date { get; set; }
        public int DonorId { get; set; }
        public int ShelterId { get; set; }

        public virtual User? User { get; set; }
        public virtual Shelter? Shelter { get; set; }
    }
}
