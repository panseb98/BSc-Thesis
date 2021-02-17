using System;
using System.Collections.Generic;
using System.Text;

namespace JobSeeker.Models
{
    public class RecommendedJobModel
    {
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public string JobShortDesc { get; set; }
        public string JobDesc { get; set; }
        public int JobRaiting { get; set; }
        public string JobSkills { get; set; }
        public string JoinedSkills { get; set; }
    }
}
