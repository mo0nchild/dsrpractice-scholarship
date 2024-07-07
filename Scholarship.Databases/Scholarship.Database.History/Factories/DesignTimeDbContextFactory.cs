using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Scholarship.Database.History.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Database.History.Factories
{
    internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<HistoryDbContext>
    {
        public DesignTimeDbContextFactory() : base() { }
        public HistoryDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), false)
                .Build();

            var connectionString = configuration.GetSection(Bootstrapper.DbSettingsSection)
                .Get<HistoryDbContextSettings>()!.ConnectionString;

            return new HistoryDbContext(DbContextOptionsFactory.Create(connectionString, false));
        }
    }
}
