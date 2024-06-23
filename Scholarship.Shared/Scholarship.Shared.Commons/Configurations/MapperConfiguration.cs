using Microsoft.Extensions.DependencyInjection;
using Scholarship.Shared.Commons.Helpers;

namespace Scholarship.Shared.Commons.Configurations;

public static class MapperConfiguration : object
{
    public static Task<IServiceCollection> AddModelsMappers(this IServiceCollection collection)
    {
        AutoMappersRegisterHelper.Register(collection);
        return Task.FromResult(collection);
    }
}