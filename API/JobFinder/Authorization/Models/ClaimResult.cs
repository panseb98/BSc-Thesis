using System;
using System.Collections.Generic;
using System.Text;

namespace Authorization.Models
{
    public class ClaimResult
    {
        public bool Succeed { get; set; }
        public object Result { get; set; }
        public Exception Exceptions { get; set; }
    }
}
