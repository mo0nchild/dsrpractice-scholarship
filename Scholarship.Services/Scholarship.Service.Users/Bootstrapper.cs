using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Scholarship.Service.Users.Infrastructure;
using Scholarship.Services.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Service.Users
{
    public static class Bootstrapper : object
    {
        public static Task<IServiceCollection> AddUsersService(this IServiceCollection collection)
        {
            collection.AddScoped<IUserService, UserService>();
            return Task.FromResult(collection);
        }
    }
}
