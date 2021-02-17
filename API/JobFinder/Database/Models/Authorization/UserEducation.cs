using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.Authorization
{
    [Table("USER_EDUCATION")]
    public class UserEducation
    {
        [Column("UED_ID")]
        public int Id { get; set; }
        [Column("UED_USER_ID")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [Column("UED_UNIVERSITY_NAME")]
        public string UniversityName { get; set; }
        [Column("UED_FIELD_OF_STUDY")]
        public string FieldOfStudy { get; set; }
        [Column("UED_STUDY_LEVEL")]
        public string StudyLevel { get; set; }
        [Column("UED_START_DATE")]
        public DateTime StartDate { get; set; }
        [Column("UED_END_DATE")]
        public DateTime? EndDate { get; set; }
        [Column("UED_IS_NOW")]
        public bool? IsNow { get; set; }
        [Column("UED_IS_ACTIVE")]
        public bool IsActive { get; set; }
    }
}
