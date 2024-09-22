using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bos.Model
{
    [Table("User")]
    public class User
    {
        [Key]
        public int AccountID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? Location { get; set; }

        public double? TotalDonate { get; set; }
        public string? Token { get; set; }

    }
}
