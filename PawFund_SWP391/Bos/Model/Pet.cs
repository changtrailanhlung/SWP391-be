using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bos.Model
{
    public class Pet : BaseEntity
    {
        public int ShelterID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Breed { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public string AdoptionStatus { get; set; }

        // Foreign key for Status
        public int StatusId { get; set; }

        public string Image { get; set; }

        // Navigation properties
        public virtual User? User { get; set; }
        public virtual AdoptionRegistrationForm? AdoptionRegistrationForm { get; set; }
        public virtual Certification? Certification { get; set; }
        public virtual Shelter? Shelter { get; set; }

        // Reference to Status
        public virtual Status? Status { get; set; }
    }
}
