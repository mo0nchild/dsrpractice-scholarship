namespace Scholarship.Shared.Commons.Helpers;

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

public static class AutoMappersRegisterHelper : object
{
    public static Task Register(IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(s => s.FullName != null && s.FullName.ToLower().StartsWith("scholarship"));

        services.AddAutoMapper(assemblies);
        return Task.CompletedTask;
    }
}