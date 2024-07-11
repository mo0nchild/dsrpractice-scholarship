
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Scholarship.Gateway
{
    public class Program : object
    {
        private static readonly string CorsName = "CorsPolicy";
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            foreach(var item in builder.Configuration.GetSection("CORS:Origins").Get<string[]>()!)
            {
                Console.WriteLine(item);
            }

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(CorsName,
                    b => b.WithOrigins(builder.Configuration.GetSection("CORS:Origins").Get<string[]>() ?? [])
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

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

            application.UseCors(CorsName);
            await application.UseOcelot();
            await application.RunAsync();
        }
    }
}
