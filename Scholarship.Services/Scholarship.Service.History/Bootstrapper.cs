using Microsoft.Extensions.DependencyInjection;
using Scholarship.Service.History.Infrastructure;

namespace Scholarship.Service.History
{
    public static class Bootstrapper : object
    {
        public static Task<IServiceCollection> AddHistoryService(this IServiceCollection collection)
        {
            collection.AddScoped<IHistoryService, HistoryService>();
            return Task.FromResult(collection);
        }
    }
}
