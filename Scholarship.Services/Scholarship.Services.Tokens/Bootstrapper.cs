using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Scholarship.Service.Tokens.Configurations;
using Scholarship.Services.Tokens.Infrastructure;

namespace Scholarship.Services.Tokens
{
    public static class Bootstrapper : object
    {
        public static Task<IServiceCollection> AddTokenService(this IServiceCollection collection, string section)
        {
            collection.AddSingleton<IConfigureOptions<TokenOptions>>(provider =>
            {
                return new ConfigureTokenOptions(provider.GetService<IConfiguration>()!, section);
            });
            collection.AddTransient<ITokenService, TokenService>();
            return Task.FromResult(collection);
        }
    }
}
