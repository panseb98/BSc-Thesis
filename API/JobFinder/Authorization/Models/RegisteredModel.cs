using Database.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authorization.Models
{
    public class RegisteredModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public User User { get; set; }
    }
}
