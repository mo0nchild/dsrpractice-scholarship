
using Scholarship.Api.Loans.Configurations;
using Scholarship.Database.Loans;
using Scholarship.Service.Loans;
using Scholarship.Shared.Commons.Helpers;
using Scholarship.Shared.Commons.Security;

namespace Scholarship.Api.Loans
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

            await builder.Services.AddLoansApiServices(builder.Configuration);
            var application = builder.Build();

            if (application.Environment.IsDevelopment())
            {
                application.UseSwagger();
                application.UseSwaggerUI();
            }
            application.UseHttpsRedirection();

            application.UseAuthentication();
            application.UseAuthorization();

            application.MapControllers();
            application.Run();
        }
    }
}
