using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Scholarship.Database.Users.Context;
using Scholarship.Database.Users.Factories;

namespace Scholarship.Database.Authorizations
{
    public static class Bootstrapper : object
    {
        public static readonly string DbSettingsSection = "Database";
        public static async Task<IServiceCollection> AddUsersDbContext(this IServiceCollection collection, 
            IConfiguration configuration = null!)
        {
            var settings = collection.Configure<UsersDbContextSettings>(configuration.GetSection(DbSettingsSection))
                .BuildServiceProvider()
                .GetRequiredService<IOptions<UsersDbContextSettings>>();
            collection.AddDbContextFactory<UsersDbContext>(options =>
            {
                DbContextOptionsFactory.Configure(settings.Value.ConnectionString, true).Invoke(options);
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
