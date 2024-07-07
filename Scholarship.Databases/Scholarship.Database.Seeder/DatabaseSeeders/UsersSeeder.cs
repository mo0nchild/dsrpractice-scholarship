using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Scholarship.Database.Users.Context;
using Scholarship.Database.Users.Entities;
using Scholarship.Service.Users.Infrastructure;
using Scholarship.Service.Users.Models;
using Scholarship.Shared.Commons.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Database.Seeder.DatabaseSeeders
{
    using UsersDbSettings = IOptions<UsersDbContextSettings>;
    public class UsersSeeder : object
    {
        private static IServiceScope GetServiceScope(IServiceProvider provider)
        {
            return provider.GetService<IServiceScopeFactory>()!.CreateScope();
        }
        private static IDbContextFactory<UsersDbContext> DbContextFactory(IServiceProvider provider)
        {
            return GetServiceScope(provider)
                .ServiceProvider.GetRequiredService<IDbContextFactory<UsersDbContext>>();
        }
        public static async Task Execute(IServiceProvider provider) => await Task.Run(async () =>
        {
            await UsersSeeder.AddIdentityRoles(provider);
            await UsersSeeder.AddAdministrator(provider);
        });
        private static async Task AddIdentityRoles(IServiceProvider provider)
        {
            using (var serviceScope = UsersSeeder.GetServiceScope(provider))
            {
                if (serviceScope == null) return;
                using var dbContext = await DbContextFactory(provider).CreateDbContextAsync();
                
                if (await dbContext.UserRoles.AnyAsync()) return;
                await dbContext.UserRoles.AddRangeAsync(new string[]
                {
                    IdentityRoleScopes.Admin,
                    IdentityRoleScopes.User
                }.Select((item, index) => new UserRole() { Uuid = Guid.NewGuid(), Name = item }));
                await dbContext.SaveChangesAsync();
            }
        }
        private static async Task AddAdministrator(IServiceProvider provider)
        {
            using (var serviceScope = UsersSeeder.GetServiceScope(provider))
            {
                if (serviceScope == null) return;
                var settings = serviceScope.ServiceProvider.GetRequiredService<UsersDbSettings>().Value;
                if (settings == null || settings.AdminInfo == null) return;

                var userService = serviceScope.ServiceProvider.GetRequiredService<IUserService>();
                if (!(await userService.IsEmpty())) return;

                await userService.Registration(new RegistrationModel()
                {
                    Email = settings.AdminInfo.Email,
                    Password = settings.AdminInfo.Password,
                    Name = "AdminUser",
                    RoleName = IdentityRoleScopes.Admin,
                });
            }
        }
    }
}
