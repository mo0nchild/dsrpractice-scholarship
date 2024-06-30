
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Scholarship.Gateway
{
    public class Program : object
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Configuration.AddJsonFile("ocelot.json");
            builder.Services.AddOcelot(builder.Configuration);

            var application = builder.Build();
            if (application.Environment.IsDevelopment())
            {
                application.UseSwagger();
                application.UseSwaggerUI();
            }
            application.UseHttpsRedirection();

            application.UseRouting();
            application.UseAuthentication();
            application.UseAuthorization();
            application.UseEndpoints(_ => { });

            await application.UseOcelot();
            await application.RunAsync();
        }
    }
}
