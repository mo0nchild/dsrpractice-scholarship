using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Scholarship.Database.History.Context;
using Scholarship.Database.History.Factories;

namespace Scholarship.Database.History
{
    public static class Bootstrapper : object
    {
        public static readonly string DbSettingsSection = "Database";
        public static async Task<IServiceCollection> AddHistoryDbContext(this IServiceCollection collection,
            IConfiguration configuration)
        {
            var settings = collection.Configure<HistoryDbContextSettings>(configuration.GetSection(DbSettingsSection))
                .BuildServiceProvider()
                .GetRequiredService<IOptions<HistoryDbContextSettings>>();
            collection.AddDbContextFactory<HistoryDbContext>(options =>
            {
                DbContextOptionsFactory.Configure(settings.Value.ConnectionString, true).Invoke(options);
            });
            var serviceProvider = collection.BuildServiceProvider();
            var dbcontextFactory = serviceProvider.GetService<IDbContextFactory<HistoryDbContext>>()!;

            using (var dbcontext = await dbcontextFactory.CreateDbContextAsync())
            {
                await dbcontext.Database.MigrateAsync();
            }
            return collection;
        }
    }
}
