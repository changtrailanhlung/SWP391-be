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
        [Key]
        public int FeedBackID { get; set; }

        [Required]
        public string Titile { get; set; }

        public string Description { get; set; }

        public DateTime PublicDate { get; set; }

        // Foreign key for User (AccountID)
        [ForeignKey("User")]
        public int AccountID { get; set; }

        // Navigation property for User
        public User? User { get; set; }

        // Foreign key for Post (PostID)
        [ForeignKey("Post")]
        public int PostID { get; set; }

        // Navigation property for Post
        public Post? Post { get; set; }
    }

}
