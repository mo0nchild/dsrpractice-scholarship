using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scholarship.Shared.Commons.Security;
using System.Security.Claims;

namespace Scholarship.Shared.Commons.Configurations
{
    public static class IdentityConfiguration : object
    {
        public static Task<IServiceCollection> AddIdentityServices(this IServiceCollection collection,
            IConfiguration configuration)
        {
            var settings = configuration.GetSection(nameof(IdentityOptions)).Get<IdentityOptions>();
            if (settings == null) throw new Exception($"{nameof(IdentityOptions)} section is empty");

            collection.AddAuthentication(UsersAuthenticateSchemeOptions.DefaultScheme)
                .AddUsersAuthentication(item => item.BaseUrl = new Uri(settings.ServiceUrl));
            collection.AddAuthorization(options =>
            {
                options.AddPolicy("User", item => item.RequireClaim(ClaimTypes.Role, new string[]
                {
                    IdentityRoleScopes.Admin,
                    IdentityRoleScopes.User
                }));
                options.AddPolicy("Admin", item => item.RequireClaim(ClaimTypes.Role, IdentityRoleScopes.Admin));
            });
            return Task.FromResult(collection);
        }
        public class IdentityOptions : object
        {
            public string ServiceUrl { get; set; } = string.Empty;
        }
    }
}
