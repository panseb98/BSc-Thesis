using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Database.Models.Authorization
{
    public class IdentityClaim<TKey> : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public IdentityClaim() { }
        public IdentityClaim(string claimKey, string claimValue, string claimType)
        {
            Key = claimKey;
            Value = claimValue;
            Type = claimType;
        }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }

        public Claim ToClaim()
        {
            return new Claim(Key, Value);
        }
    }
}
