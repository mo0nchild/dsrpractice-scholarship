using Microsoft.EntityFrameworkCore;
using Scholarship.Database.Users.Configurations;
using Scholarship.Database.Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Database.Users.Context
{
    public class UsersDbContext : DbContext
    {
        public virtual DbSet<UserRole> UserRoles { get; set; } = default!;
        public virtual DbSet<UserInfo> UserInfos { get; set; } = default!;
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; } = default!;

        public UsersDbContext(DbContextOptions<UsersDbContext> options) 
            : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseLazyLoadingProxies());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
