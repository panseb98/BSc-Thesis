using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models
{
    [Table("JOB_SEEKER")]
    public class Jobseeker
    {
        [Column("JS_ID")]
        public int Id { get; set; }
        [Column("JS_FIRST_NAME")]
        public string FirstName { get; set; }
        [Column("JS_SECOND_NAME")]
        public string SecondName { get; set; }    
        [Column("JS_CITY")]
        public string City { get; set; }
        [Column("JS_PHONE")]
        public string PhoneNumber { get; set; }
        
    }
}
