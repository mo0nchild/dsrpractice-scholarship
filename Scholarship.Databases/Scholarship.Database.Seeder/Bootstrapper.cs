
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scholarship.Database.Seeder.DatabaseSeeders;

namespace Scholarship.Database.Seeder
{
    public static class Bootstrapper : object
    {
        public static async Task<IServiceCollection> AddDbSeeder(this IServiceCollection collection, 
            IConfiguration configuration)
        {
            await UsersSeeder.Execute(collection.BuildServiceProvider());
            return collection;
        }
    }
}
