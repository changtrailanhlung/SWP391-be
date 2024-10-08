﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Models
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string Token { get; set; }
        public decimal TotalDonation { get; set; }
        public string? Image { get; set; }
    }
}
