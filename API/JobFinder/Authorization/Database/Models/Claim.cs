using System;
using System.Collections.Generic;
using System.Text;


namespace Authorization.Database.Models
{
    public class Claim
    {
        public Claim() { }
        public Claim(string claimKey, string claimValue, string claimType)
        {
            Key = claimKey;
            Value = claimValue;
            Type = claimType;
        }
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }

        public System.Security.Claims.Claim ToClaim()
        {
            return new System.Security.Claims.Claim(Key, Value);
        }
    }
}
