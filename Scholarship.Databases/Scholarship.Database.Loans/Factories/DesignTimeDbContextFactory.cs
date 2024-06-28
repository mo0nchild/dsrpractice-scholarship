using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Scholarship.Database.Loans.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Database.Loans.Factories
{
    internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LoansDbContext>
    {
        public DesignTimeDbContextFactory() : base() { }
        public LoansDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), false)
                .Build();

            var connectionString = configuration.GetSection(Bootstrapper.DbSettingsSection)
                .Get<LoansDbContextSettings>()!.ConnectionString;

            return new LoansDbContext(DbContextOptionsFactory.Create(connectionString, false));
        }
    }
}
