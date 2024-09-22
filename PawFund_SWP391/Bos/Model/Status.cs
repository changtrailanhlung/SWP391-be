using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bos.Model
{
    [Table("Status")]
    public class Status 
    {
        [Key]
        public int StatusId { get; set; } 
        [Required]
        public string Name { get; set; }
        [Required]
        public string? Date { get; set; }
        public string? Disease { get; set; }
        public string Vaccine { get; set; }

        // Remove the Pet reference if not needed, or configure it properly for the one-to-one relationship
    }
}
