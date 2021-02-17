using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.InitJobOffers
{
    [Table("JOB_TITLE")]
    public class JobTitle
    {
        [Column("JOBT_ID")]
        public int Id { get; set; }
        [Column("JOBT_NAME")]
        public string Name { get; set; }
    }
}
