using System;
using System.Collections.Generic;
using System.Text;

namespace JobSeeker.Models
{
    public class RequestList
    {
        public List<NewKey> KeyRequests { get; set; }
        public List<NewJob> JobRequests { get; set; }
    }
}
