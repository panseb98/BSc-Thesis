using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.Authorization
{
    [Table("USER_PERSONAL_DATA")]
    public class UserPersonalData
    {
        [Column("UPD_ID")]
        public int Id { get; set; }
        [Column("UPD_USER_ID")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [Column("UPD_FIRST_NAME")]
        public string FirstName { get; set; }
        [Column("UPD_LAST_NAME")]
        public string LastName { get; set; }
    }
}
