using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.InitJobOffers
{
    [Table("JOB_OFFER")]
    public class JobOffer
    {
        [Column("JOB_ID")]
        public int Id { get; set; }
        [Column("JOB_NAME")]
        public string JobName { get; set; }
        [Column("JOB_LOCATION")]
        public string JobLocation { get; set; }
        [Column("JOB_COMPANY")]
        public string JobCompany { get; set; }
        [Column("JOB_DESC")]
        public string JobDesc { get; set; }
        [Column("JOB_KEY")]
        public string JobKey { get; set; }
        [Column("JOB_FROM")]
        public string JobFrom { get; set; }
        [Column("JOB_KEYS")]
        public string Keys { get; set; }
    }
}
