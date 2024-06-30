using MassTransit;
using Scholarship.Service.Backup;
using Scholarship.Shared.Commons.Configurations;
using Scholarship.Shared.Commons.Helpers;
using Scholarship.Shared.Commons.Security;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Scholarship.Api.Backup.Configurations
{
    public static class ApiServicesConfiguration : object
    {
        public async static Task<IServiceCollection> AddBackupApiServices(this IServiceCollection collection,
            IConfiguration configuration)
        {
            await collection.AddTransitServices(configuration);
            await collection.AddModelsValidators();
            await collection.AddModelsMappers();

            await collection.AddBackupService();
            return collection;
        }
    }
}
