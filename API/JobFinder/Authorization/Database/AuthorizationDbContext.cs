using Authorization.Database.Models;
using Database.Models.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authorization.Database
{
    public class AuthorizationDbContext : IdentityDbContext<User,
                                                            Role, 
                                                            int,
                                                            UserClaim,
                                                            UserRole,
                                                            UserLogin,
                                                            RoleClaim,
                                                            UserToken>
    {
        public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options) : base(options)
        {
               
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(e =>
            {
                e.ToTable("USER");
                e.HasKey(b => b.Id);
                e.Property(b => b.Id); 
                e.Property(p => p.Email)
                    .HasMaxLength(50);
                e.Property(p => p.UserName)
                    .HasMaxLength(50);
                e.Property(p => p.ConcurrencyStamp)
                    .HasMaxLength(512);
                e.Property(p => p.SecurityStamp)
                    .HasMaxLength(512);
                e.Property(p => p.PasswordHash)
                    .HasMaxLength(1024);
                e.Property(p => p.PhoneNumber)
                    .HasMaxLength(15);
                e.Property(p => p.NormalizedUserName)
                    .HasMaxLength(50);
                e.Property(p => p.NormalizedEmail)
                    .HasMaxLength(50);

                e.Property(p => p.FirstName)
                    .HasMaxLength(50);
                e.Property(p => p.LastName)
                    .HasMaxLength(50);

                e.Ignore(p => p.PhoneNumberConfirmed);
                e.Ignore(p => p.TwoFactorEnabled);
                e.Ignore(p => p.AccessFailedCount);
                e.Ignore(p => p.LockoutEnabled);
                e.Ignore(p => p.NormalizedEmail);
                e.Ignore(p => p.NormalizedUserName);
                e.Ignore(p => p.LockoutEnd);
                e.Ignore(p => p.EmailConfirmed);

            });

            builder.Entity<UserClaim>(e =>
            {
                e.ToTable("USER_CLAIMS");

                e.HasKey(e => e.Id);

                e.Property(e => e.Id)
                    .HasColumnName("ID");
                e.Property(p => p.UserId)
                    .HasColumnName("USERID");
                e.Property(p => p.ClaimType)
                    .HasColumnName("CLAIMTYPE")
                    .HasMaxLength(50);
                e.Property(p => p.ClaimValue)
                    .HasColumnName("CLAIMVALUE")
                    .HasMaxLength(50);
            });


            
            builder.Entity<Claim>(b =>
            {
                b.ToTable("CLAIMS");
                b.Property(p => p.Key)
                .HasMaxLength(50);
                b.Property(p => p.Value)
                .HasMaxLength(50);
                b.Property(p => p.Type)
                .HasMaxLength(20);
            });

            builder.Ignore<UserToken>();
            builder.Ignore<Role>();
            builder.Ignore<UserRole>();
            builder.Ignore<UserLogin>();
            builder.Ignore<RoleClaim>();

        }
    }
}
