using Microsoft.EntityFrameworkCore;
using Scholarship.Database.History.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Database.History.Factories
{
    public static class DbContextOptionsFactory : object
    {
        public static DbContextOptions<HistoryDbContext> Create(string connectionString, bool detailedLogging = false)
        {
            var builder = new DbContextOptionsBuilder<HistoryDbContext>();
            DbContextOptionsFactory.Configure(connectionString, detailedLogging).Invoke(builder);

            return builder.Options;
        }
        public static Action<DbContextOptionsBuilder> Configure(string connectionString, bool detailedLogging = false)
        {
            return (builder) =>
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
