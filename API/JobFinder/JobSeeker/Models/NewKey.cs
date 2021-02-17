using System;
using System.Collections.Generic;
using System.Text;

namespace JobSeeker.Models
{
    public class NewKey
    {
        public int? Id { get; set; }
        public string KeyName { get; set; }
        public string UserId { get; set; }
        public DateTime? Date { get; set; }
    }
}
