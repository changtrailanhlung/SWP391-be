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
        [Key] public int NotificationID { get; set; }
        public string? Message { get; set; }
        [Required]
        public string Date { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User? User { get; set; }
    }

}
