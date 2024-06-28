using MassTransit;
using Scholarship.Database.Authorizations;
using Scholarship.Database.Seeder;
using Scholarship.Service.Users;
using Scholarship.Services.Tokens;
using Scholarship.Shared.Commons.Configurations;
using Scholarship.Shared.Commons.Helpers;
using System.Reflection;
using System.Runtime.CompilerServices;


namespace Scholarship.Api.Users.Configurations
{
    public static class ApiServicesConfiguration : object
    {
        public async static Task<IServiceCollection> AddUsersApiServices(this IServiceCollection collection, 
            IConfiguration configuration)
        {
            await collection.AddTransitServices(configuration);
            await collection.AddModelsValidators();
            await collection.AddModelsMappers();

            await collection.AddTokenService("TokenSettings");
            await collection.AddUsersDbContext(configuration);
            await collection.AddUsersService();

            await collection.AddDbSeeder(configuration);
            return collection;
        } 
    }
}
