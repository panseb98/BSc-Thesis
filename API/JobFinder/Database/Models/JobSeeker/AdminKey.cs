using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.JobSeeker
{
    public class AdminKey
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("KEY_VALUE")]
        public string KeyValue { get; set; }
        [Column("KEY_TYPE")]
        public string KeyType { get; set; }
    }
}
