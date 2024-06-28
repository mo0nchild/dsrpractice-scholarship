using Microsoft.EntityFrameworkCore;
using Scholarship.Database.Users.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Database.Users.Factories
{
    public static class DbContextOptionsFactory : object
    {
        public static DbContextOptions<UsersDbContext> Create(string connectionString, bool detailedLogging = false)
        {
            var builder = new DbContextOptionsBuilder<UsersDbContext>();
            DbContextOptionsFactory.Configure(connectionString, detailedLogging).Invoke(builder);

            return builder.Options;
        }
        public static Action<DbContextOptionsBuilder> Configure(string connectionString, bool detailedLogging = false)
        {
            return (DbContextOptionsBuilder builder) =>
            {
                builder.UseNpgsql(connectionString, options =>
                {
                    options.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                        .MigrationsHistoryTable("_Migrations");
                }); 
                if (detailedLogging) builder.EnableSensitiveDataLogging();
                builder.UseLazyLoadingProxies(true);
            };
        }
    }
}
