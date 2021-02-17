using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.Authorization
{
    [Table("USER")]

    public class User : IdentityUser<int>
    {
        #region OverriderIdentityFields
        [Column("USER_ID")]
        public override int Id { get; set; }
        [Column("USER_EMAIL")]
        public override string Email { get => base.Email; set => base.Email = value; }
        [Column("USER_USER_NAME")]
        public override string UserName { get => base.UserName; set => base.UserName = value; }
        [Column("USER_PHONE")]
        public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }
        [Column("USER_CONCURRENCY_STAMP")]
        public override string ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }
        [Column("USER_PW_HASH")]
        public override string PasswordHash { get => base.PasswordHash; set => base.PasswordHash = value; }

        [Column("USER_SECURITY_STAMP")]
        public override string SecurityStamp { get => base.SecurityStamp; set => base.SecurityStamp = value; }
        [Column("USER_FIRST_NAME")]
        public string FirstName { get; set; }
        [Column("USER_LAST_NAME")]
        public string LastName { get; set; }
        #endregion
        public virtual ICollection<UserEducation> UserEducations { get; set; }
        public virtual ICollection<UserExperience> UserExperiences { get; set; }
        public virtual ICollection<UserSkill> UserSkills { get; set; }
        [Column("USER_MODIFY_DATE")]
        public DateTime ModyficationDate { get; set; }
        [Column("USER_IS_ACTIVE")]
        public bool IsActive { get; set; }
       

        #region IgnoredIndentityFields
        [NotMapped]
        public override int AccessFailedCount { get => base.AccessFailedCount; set => base.AccessFailedCount = value; }
        [NotMapped]
        public override bool EmailConfirmed { get => base.EmailConfirmed; set => base.EmailConfirmed = value; }
        [NotMapped]
        public override bool PhoneNumberConfirmed { get => base.PhoneNumberConfirmed; set => base.PhoneNumberConfirmed = value; }
        [NotMapped]
        public override bool TwoFactorEnabled { get => base.TwoFactorEnabled; set => base.TwoFactorEnabled = value; }
        [NotMapped]
        public override bool LockoutEnabled { get => base.LockoutEnabled; set => base.LockoutEnabled = value; }
        [NotMapped]
        public override string NormalizedEmail { get => base.NormalizedEmail; set => base.NormalizedEmail = value; }
        [NotMapped]
        public override string NormalizedUserName { get => base.NormalizedUserName; set => base.NormalizedUserName = value; }
        [NotMapped]
        public override DateTimeOffset? LockoutEnd { get => base.LockoutEnd; set => base.LockoutEnd = value; }

        #endregion
    }
}
