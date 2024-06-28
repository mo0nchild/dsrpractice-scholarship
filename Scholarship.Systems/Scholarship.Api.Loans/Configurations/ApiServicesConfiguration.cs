﻿using MassTransit;
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
                options.AddPolicy("User", item => item.RequireClaim(ClaimTypes.Role, new string[]
                {
                    IdentityRoleScopes.Admin,
                    IdentityRoleScopes.User
                }));
                options.AddPolicy("Admin", item => item.RequireClaim(ClaimTypes.Role, IdentityRoleScopes.Admin));
            });
            await collection.AddTransitServices(configuration);
            await collection.AddModelsValidators();
            await collection.AddModelsMappers();

            await collection.AddLoansDbContext(configuration);
            await collection.AddLoanService();

            return collection;
        }
    }
}
