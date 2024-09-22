using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bos.Model
{
    public class Status : BaseEntity
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string Disease { get; set; }
        public string Vaccine { get; set; }

        // Remove the Pet reference if not needed, or configure it properly for the one-to-one relationship
    }
}
