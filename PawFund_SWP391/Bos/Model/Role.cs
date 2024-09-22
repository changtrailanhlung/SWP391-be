using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bos.Model
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<User>? Users { get; set; } = new List<User>();

    }
}
