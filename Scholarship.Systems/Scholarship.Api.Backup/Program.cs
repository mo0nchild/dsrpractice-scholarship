
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Scholarship.Api.Backup.Configurations;
using Scholarship.Shared.Commons.Configurations;
using Scholarship.Shared.Commons.Exceptions;
using Scholarship.Shared.Commons.Middlewares;
using static System.Net.Mime.MediaTypeNames;

namespace Scholarship.Api.Backup
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

            await builder.Services.AddBackupApiServices(builder.Configuration);
            await builder.Services.AddIdentityServices(builder.Configuration);
            var application = builder.Build();

            if (application.Environment.IsDevelopment())
            {
                application.UseSwagger();
                application.UseSwaggerUI();
            }
            application.UseHttpsRedirection();
            application.UseExceptionsHandler<ValidationException>();
            application.UseExceptionsHandler<ProcessException>();

            application.UseAuthentication();
            application.UseAuthorization();
            application.MapControllers();
            await application.RunAsync();
        }
    }
}
