using Database.Models;
using Database.Models.Authorization;
using Database.Models.InitJobOffers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Database.Models.JobSeeker;

namespace Database
{
    public class DataContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContext;

        public DataContext(DbContextOptions<DataContext> options, IHttpContextAccessor httpContext = null) : base(options)
        {
            _httpContext = httpContext;
        }

        #region Jobs
        public DbSet<JobOffer> JobOffers { get; set; }
        public DbSet<JobSkill> JobSkills { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<JobCity> JobCities { get; set; }
        public DbSet<KeyRequest> KeyRequests { get; set; }
        public DbSet<JobRequest> JobRequests { get; set; }
        public DbSet<AdminKey> AdminKeys { get; set; }

        #endregion

        #region Authorization
        public DbSet<UserEducation> UserEducations { get; set; }
        public DbSet<UserExperience> UserExperiences { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        #endregion

        public void DeleteChanges() => ChangeTracker
            .Entries()
            .Where(x => x.Entity != null)
            .ToList()
            .ForEach(x => x.State = EntityState.Detached);
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserEducation>(e =>
            {
                e.ToTable("USER_EDUCATION");
                e.HasKey(b => b.Id);
                e.Property(b => b.Id);
                e.Property(p => p.FieldOfStudy);
                e.Property(b => b.EndDate);
                e.Property(b => b.IsActive);
                e.Property(b => b.IsNow);
                e.Property(b => b.StartDate);
                e.Property(b => b.StudyLevel);
                e.Property(b => b.UserId);
                e.Property(b => b.UniversityName);
            });

            builder.Entity<UserExperience>(e =>
            {
                e.ToTable("USER_EXPERIENCE");
                e.HasKey(b => b.Id);
                e.Property(b => b.Id);
                e.Property(p => p.PositionName);
                e.Property(b => b.EndDate);
                e.Property(b => b.IsActive);
                e.Property(b => b.IsNow);
                e.Property(b => b.StartDate);
                e.Property(b => b.Description);
                e.Property(b => b.UserId);
                e.Property(b => b.CompanyName);
            });

            builder.Entity<UserSkill>(e =>
            {
                e.ToTable("USER_SKILL");
                e.HasKey(b => b.Id);
                e.Property(b => b.Id);
                e.Property(p => p.SkillId);
                e.Property(b => b.UserId);
            });

            builder.Entity<JobCity>(e =>
            {
                e.ToTable("JOB_CITY");
                e.HasKey(b => b.Id);
                e.Property(b => b.Id);
                e.Property(b => b.Name);

            });

            builder.Entity<JobOffer>(e =>
            {
                e.ToTable("JOB_OFFER");
                e.HasKey(b => b.Id);
                e.Property(b => b.Id);
                e.Property(b => b.JobCompany);
                e.Property(b => b.JobDesc);
                e.Property(b => b.JobFrom);
                e.Property(b => b.JobKey);
                e.Property(b => b.JobLocation);
                e.Property(b => b.JobName);
                e.Property(b => b.Keys);
            });

            builder.Entity<JobSkill>(e =>
            {
                e.ToTable("JOB_SKILL");
                e.HasKey(b => b.Id);
                e.Property(b => b.Id);
                e.Property(b => b.Key);
            });
            builder.Entity<JobRequest>(e =>
            {
                e.ToTable("JOB_REQUEST");
                e.HasKey(b => b.Id);
                e.Property(b => b.Id);
                e.Property(b => b.JobName);
                e.Property(b => b.Date);
                e.Property(b => b.Location);
                e.Property(b => b.UserId);

            });
            builder.Entity<KeyRequest>(e =>
            {
                e.ToTable("KEY_REQUEST");
                e.HasKey(b => b.Id);
                e.Property(b => b.Id);
                e.Property(b => b.KeyName);
                e.Property(b => b.Date);
                e.Property(b => b.UserId);

            });

            builder.Entity<JobTitle>(e =>
            {
                e.ToTable("JOB_TITLE");
                e.HasKey(b => b.Id);
                e.Property(b => b.Id);
                e.Property(b => b.Name);
            });
            builder.Entity<AdminKey>(e =>
            {
                e.ToTable("ADMIN_KEY");
                e.HasKey(b => b.Id);
                e.Property(b => b.KeyType);
                e.Property(b => b.KeyValue);

            });
        }
        public string GetUserEmail()
        {
            return _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.GivenName);
        }
    }
}
