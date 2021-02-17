using System;
using System.Collections.Generic;
using System.Text;

namespace JobSeeker.Models
{
    public class ExperienceUser
    {
        public int? Id { get; set; }
        public string CompanyName { get; set; }
        public string PositionName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsNow { get; set; }
    }

}
