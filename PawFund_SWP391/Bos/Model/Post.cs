using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bos.Model
{
    [Table("Post")]
    public class Post
    {
        [Key]
        public string PostID {  get; set; }   

        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DateCreate { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int AccountID { get; set; }  // Foreign key

        // Navigation property
        public User? User { get; set; }
    }
}
