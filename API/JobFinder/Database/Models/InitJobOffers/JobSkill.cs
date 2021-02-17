using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models
{
    [Table("JOB_SKILL")]
    public class JobSkill
    {
        [Column("JSL_ID")]
        public int Id { get; set; }
        [Column("JSL_KEY")]
        public string Key { get; set; }
    }
}
