using Database.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database.Models
{
    public class BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
        [DateTimeKind(DateTimeKind.Utc)]
        public DateTime ModifiedDate { get; set; }
        [MaxLength(50)]
        public string ModifiedUserName { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
