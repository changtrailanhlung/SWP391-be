using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bos.Model
{
    public class Certification : BaseEntity
    {
        public string Image { get; set; }
        public string Desciption { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int PetId { get; set; }

        public virtual User? User { get; set; }
        public virtual Pet? Pet { get; set; }
    }
}
}
