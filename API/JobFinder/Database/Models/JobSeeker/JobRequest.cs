using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.JobSeeker
{
    public class JobRequest
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("KEY_NAME")]
        public string JobName { get; set; }
        [Column("KEY_LOCATION")]
        public string Location { get; set; }
        [Column("DATE")]
        public DateTime Date { get; set; }
        [Column("USER_ID")]
        public int UserId { get; set; }
    }
}
