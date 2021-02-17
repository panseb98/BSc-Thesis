using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.Authorization
{
    public class Role : IdentityRole<int>
    {
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
    }
}
