using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.InitJobOffers
{
    [Table("JOB_CITY")]
    public class JobCity
    {
        [Column("JOBC_ID")]
        public int Id { get; set; }
        [Column("JOBC_NAME")]
        public string Name { get; set; }
    }
}
