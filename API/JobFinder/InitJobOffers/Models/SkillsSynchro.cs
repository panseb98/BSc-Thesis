using System;
using System.Collections.Generic;
using System.Text;

namespace InitJobOffers.Models
{
    public class SkillsSynchro
    {
        public bool Success { get; set; }
        public string Category { get; set; }
        public int NumValues { get; set; }
        public List<SkillSynchroKeyword> Keywords { get; set; }
        public SkillSynchroLink Links { get; set; }


    }
    public class SkillSynchroKeyword
    {
        public int Id { get; set; }
        public string KeyName { get; set; }
    }

    public class SkillSynchroLink
    {
        public List<SkillSychroSelf> Self { get; set; }
    }

    public class SkillSychroSelf
    {
        public string Href { get; set; }
        public string Rel { get; set; }
    }
}
