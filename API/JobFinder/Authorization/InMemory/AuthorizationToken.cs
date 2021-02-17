using System;
using System.Collections.Generic;
using System.Text;

namespace Authorization.InMemory
{
    public class AuthorizationToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        public bool IsAlive { get; set; }
    }
}
