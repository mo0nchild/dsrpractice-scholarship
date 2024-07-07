
using FluentValidation;
using Scholarship.Api.Users.Configurations;
using Scholarship.Database.Authorizations;
using Scholarship.Service.Users;
using Scholarship.Services.Tokens;
using Scholarship.Shared.Commons.Exceptions;
using Scholarship.Shared.Commons.Helpers;
using Scholarship.Shared.Commons.Middlewares;

namespace Scholarship.Api.Users
{
    public class Program : object
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddHttpClient();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            await builder.Services.AddUsersApiServices(builder.Configuration);
            var application = builder.Build();
            
            if (application.Environment.IsDevelopment())
            {
                application.UseSwagger();
                application.UseSwaggerUI();
            }
            application.UseHttpsRedirection();
            application.UseExceptionsHandler();

            application.UseAuthorization();
            application.MapControllers();
            await application.RunAsync();
        }
    }
}
