using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scholarship.Database.Loans.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Database.Loans
{
    public static class Bootstrapper : object
    {
        public static readonly string DatabaseName = "scholarship.loans";
        public static async Task<IServiceCollection> AddLoansDbContext(this IServiceCollection collection, 
            IConfiguration configuration = null!)
        {
            collection.AddDbContextFactory<LoansDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(DatabaseName));
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
