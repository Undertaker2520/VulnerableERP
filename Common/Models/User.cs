using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class User
    {
        public int id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public bool isAdmin { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
