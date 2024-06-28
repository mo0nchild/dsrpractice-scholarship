using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Scholarship.Database.Loans.Context;
using Scholarship.Database.Loans.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Database.Loans
{
    public static class Bootstrapper : object
    {
        public static readonly string DbSettingsSection = "Database";
        public static async Task<IServiceCollection> AddLoansDbContext(this IServiceCollection collection, 
            IConfiguration configuration = null!)
        {
            var settings = collection.Configure<LoansDbContextSettings>(configuration.GetSection(DbSettingsSection))
                .BuildServiceProvider()
                .GetRequiredService<IOptions<LoansDbContextSettings>>();
            collection.AddDbContextFactory<LoansDbContext>(options =>
            {
                DbContextOptionsFactory.Configure(settings.Value.ConnectionString, true).Invoke(options);
            });
            var serviceProvider = collection.BuildServiceProvider();
            var dbcontextFactory = serviceProvider.GetService<IDbContextFactory<LoansDbContext>>()!;

            using (var dbcontext = await dbcontextFactory.CreateDbContextAsync())
            {
                await dbcontext.Database.MigrateAsync();
            }
            return collection;
        }
    }
}
