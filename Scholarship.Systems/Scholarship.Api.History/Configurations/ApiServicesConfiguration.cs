using MassTransit;
using Scholarship.Database.History;
using Scholarship.Service.History;
using Scholarship.Shared.Commons.Configurations;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Scholarship.Api.History.Configurations
{
    public static class ApiServicesConfiguration : object
    {
        public async static Task<IServiceCollection> AddHistoryApiServices(this IServiceCollection collection,
            IConfiguration configuration)
        {
            await collection.AddTransitServices(configuration);
            await collection.AddModelsValidators();
            await collection.AddModelsMappers();

            await collection.AddHistoryDbContext(configuration);
            await collection.AddHistoryService();
            return collection;
        }
    }
}
