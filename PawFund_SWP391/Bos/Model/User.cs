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
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        public string? Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Phone { get; set; }
        public string? Location { get; set; }
        public string Token { get; set; }
        public string? TotalDonation { get; set; }
        public string? Immage { get; set; }

        [ForeignKey("Role")]
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ShelterStaff? ShelterStaff { get; set; }

        //public virtual ICollection<Pet>? Pets { get; set; }
        public virtual ICollection<AdoptionRegistrationForm>? AdopteRegister_Forms { get; set; }
        public virtual ICollection<Notification>? Notifications { get; set; }
        public virtual ICollection<Post>? Posts { get; set; }
        public virtual ICollection<Donation>? Donations { get; set; }
        public virtual ICollection<Event>? Events { get; set; }
        public virtual ICollection<Certification>? Certifications { get; set; }

    }
}
