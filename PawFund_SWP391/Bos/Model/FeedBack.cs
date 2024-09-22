using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bos.Model
{
    [Table("FeedBack")]
    public class FeedBack 
    {
        [Key] public int FeedBackID { get; set; }
        [ForeignKey("Post")]
        public int PostID { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public string? Description { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public virtual Post? Post { get; set; }
        public virtual User? User { get; set; }  
    }

}
