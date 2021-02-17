using System;
using System.Collections.Generic;
using System.Text;

namespace Authorization.Models
{
    public class LoggedUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public Dictionary<string, string> Claim { get; set; }
    }
}
