using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bos.Model
{
    public class Notification : BaseEntity
    {
        public string Message { get; set; }
        public string Date { get; set; }
        public string UserId { get; set; }

        public virtual User? User { get; set; }
    }

}
