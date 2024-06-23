using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scholarship.Database.Users.Context;

namespace Scholarship.Database.Authorizations
{
    public static class Bootstrapper : object
    {
        public static readonly string DatabaseName = "scholarship.users";
        public static async Task<IServiceCollection> AddUsersDbContext(this IServiceCollection collection, 
            IConfiguration configuration = null!)
        {
            collection.AddDbContextFactory<UsersDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(DatabaseName));
            });
            var serviceProvider = collection.BuildServiceProvider();
            var dbcontextFactory = serviceProvider.GetService<IDbContextFactory<UsersDbContext>>()!;

            using (var dbcontext = await dbcontextFactory.CreateDbContextAsync())
            {
                await dbcontext.Database.MigrateAsync();
            }
            return collection;
        }
    }
}
