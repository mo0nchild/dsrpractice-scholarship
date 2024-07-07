
using Microsoft.AspNetCore.Builder;
using Scholarship.Api.History.Configurations;
using Scholarship.Service.History;
using Scholarship.Shared.Commons.Configurations;
using Scholarship.Shared.Commons.Middlewares;

namespace Scholarship.Api.History
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddHttpClient();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            await builder.Services.AddHistoryApiServices(builder.Configuration);
            await builder.Services.AddIdentityServices(builder.Configuration);
            var application = builder.Build();

            if (application.Environment.IsDevelopment())
            {
                application.UseSwagger();
                application.UseSwaggerUI();
            }
            application.UseHttpsRedirection();
            application.UseExceptionsHandler();

            application.UseAuthentication();
            application.UseAuthorization();
            application.MapControllers();
            await application.RunAsync();
        }
    }
}
