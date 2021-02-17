using System;
using System.Collections.Generic;
using System.Text;

namespace JobSeeker.Models
{
    public class ExpendedUserData
    {
         public int UserId { get; set; }
         public List<EducationUser> Educations { get; set; }
         public List<ExperienceUser> Experiences { get; set; }
         public List<SkillUser> Skills { get; set; }
    }
}
