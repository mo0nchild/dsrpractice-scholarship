using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Scholarship.Database.Authorizations;
using Scholarship.Database.Users.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Database.Users.Factories
{
    internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<UsersDbContext>
    {
        public static readonly string DatabaseName = "scholarship.users";
        public DesignTimeDbContextFactory() : base() { }
        public UsersDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), false)
                .Build();

            var connectionString = configuration.GetSection(Bootstrapper.DbSettingsSection)
                .Get<UsersDbContextSettings>()!.ConnectionString;

            return new UsersDbContext(DbContextOptionsFactory.Create(connectionString, false));
        }
    }
}
