using System;
using System.Collections.Generic;
using System.Text;

namespace JobSeeker.Models
{
    public class NewJob
    {
        public int? Id { get; set; }
        public string KeyName { get; set; }
        public string Location { get; set; }
        public string UserId { get; set; }
        public DateTime? Date { get; set; }

    }
}
