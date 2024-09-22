using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bos.Model
{
    [Table("Notification")]
    public class Notification
    {
        [Key]
        public int NotificateID { get; set; }

        public string? Message { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        // Foreign key referencing User table
        [ForeignKey("User")]
        public int AccountID { get; set;     }  // Foreign key

        // Navigation property
        public User? User { get; set; }
    }

}
