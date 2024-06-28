using MassTransit;
using Scholarship.Database.Loans;
using Scholarship.Service.Loans;
using Scholarship.Shared.Commons.Configurations;
using Scholarship.Shared.Commons.Helpers;
using Scholarship.Shared.Commons.Security;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Scholarship.Api.Loans.Configurations
{
    public static class ApiServicesConfiguration : object
    {
        public async static Task<IServiceCollection> AddLoansApiServices(this IServiceCollection collection,
            IConfiguration configuration)
        {
            collection.AddAuthentication(UsersAuthenticateSchemeOptions.DefaultScheme)
                .AddUsersAuthentication(item =>
                {
                    item.BaseUrl = new Uri("http://scholarship.users:8080");
                });
            collection.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", item => item.RequireClaim(ClaimTypes.Role, IdentityRoleScopes.Admin));
                options.AddPolicy("User", item => item.RequireClaim(ClaimTypes.Role, IdentityRoleScopes.User));
            });
            collection.AddMassTransit(options =>
            {
                options.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq", "/", host =>
                    {
                        host.Username("admin");
                        host.Password("1234567890");
                    });
                    cfg.ConfigureEndpoints(context);
                    cfg.UseRawJsonSerializer();
                });
            });
            await collection.AddModelsValidators();
            await collection.AddModelsMappers();

            await collection.AddLoansDbContext(configuration);
            await collection.AddLoanService();

            return collection;
        }
    }
}
