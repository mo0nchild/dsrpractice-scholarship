using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Database.Users.Context
{
    internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<UsersDbContext>
    {
        public DesignTimeDbContextFactory() : base() { }
        public UsersDbContext CreateDbContext(string[] args)
        {
            var contextOptions = new DbContextOptionsBuilder<UsersDbContext>();
            contextOptions.UseNpgsql("Server=localhost;Port=5433;Username=postgres;Password=1234567890;Database=scholarship.authorizations");

            return new UsersDbContext(contextOptions.Options);
        }
    }
}
