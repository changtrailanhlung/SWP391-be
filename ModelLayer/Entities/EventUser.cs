﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Entities
{
    public class EventUser
    {
        public int UserId { get; set; }
        public int EventId { get; set; }

        public virtual User? User { get; set; }
        public virtual Event? Event { get; set; }
    }
}
