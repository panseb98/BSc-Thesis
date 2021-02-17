using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authorization.InMemory
{
    public class InMemoryDatabase : DbContext
    {
        public InMemoryDatabase(DbContextOptions<InMemoryDatabase> options): base(options)
        {
        }
        public DbSet<AuthorizationToken> Tokens { get; set; }
    }
}
