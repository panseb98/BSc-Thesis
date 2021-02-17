using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.Authorization
{
    [Table("USER_EXPERIENCE")]
    public class UserExperience
    {
        [Column("UEX_ID")]
        public int Id { get; set; }
        [Column("UEX_USER_ID")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [Column("UEX_COMPANY_NAME")]
        public string CompanyName { get; set; }
        [Column("UEX_POSITION_NAME")]
        public string PositionName { get; set; }
        [Column("UEX_LOCALIZATION")]
        public string Description { get; set; }
        [Column("UEX_START_DATE")]
        public DateTime StartDate { get; set; }
        [Column("UEX_END_DATE")]
        public DateTime? EndDate { get; set; }
        [Column("UEX_IS_NOW")]
        public bool? IsNow { get; set; }
        [Column("UEX_IS_ACTIVE")]
        public bool IsActive { get; set; }
        
    }
}
