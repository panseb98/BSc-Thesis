using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.Authorization
{
    [Table("USER_SKILL")]
    public class UserSkill
    {
        [Column("US_ID")]
        public int Id { get; set; }

        [Column("US_USER_ID")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [Column("US_SKILL_ID")]
        public int SkillId { get; set; }

        public virtual JobSkill Skill { get; set; }

    }
}
