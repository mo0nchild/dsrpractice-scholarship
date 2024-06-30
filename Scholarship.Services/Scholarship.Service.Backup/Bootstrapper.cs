using Microsoft.Extensions.DependencyInjection;
using Scholarship.Service.Backup.Infrastructure;

namespace Scholarship.Service.Backup
{
    public static class Bootstrapper : object
    {
        public static Task<IServiceCollection> AddBackupService(this IServiceCollection collection)
        {
            collection.AddScoped<IBackupService, BackupService>();
            return Task.FromResult(collection);
        }
    }
}
