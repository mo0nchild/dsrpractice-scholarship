namespace Scholarship.Shared.Commons.Helpers;

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

public static class ValidatorsRegisterHelper : object
{
    public static Task Register(IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(s => s.FullName != null && s.FullName.ToLower().StartsWith("scholarship."));

        assemblies.ToList().ForEach(x => { services.AddValidatorsFromAssembly(x, ServiceLifetime.Scoped); });
        return Task.CompletedTask;
    }
}