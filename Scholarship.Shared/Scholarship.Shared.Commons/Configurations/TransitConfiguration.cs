using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Shared.Commons.Configurations
{
    public static class TransitConfiguration : object
    {
        public static Task<IServiceCollection> AddTransitServices(this IServiceCollection collection,
            IConfiguration configuration)
        {
            var settings = configuration.GetSection(nameof(TransitOptions)).Get<TransitOptions>();
            if (settings == null) throw new Exception($"{nameof(TransitOptions)} section is empty");
            collection.AddMassTransit(options =>
            {
                options.AddConsumers(Assembly.GetEntryAssembly());
                options.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(settings.Host, settings.VirtualHost, host =>
                    {
                        host.Username(settings.User);
                        host.Password(settings.Password);
                    });
                    cfg.ConfigureEndpoints(context);
                    cfg.UseRawJsonSerializer();
                });
            });
            return Task.FromResult(collection);
        }
    }
    public class TransitOptions : object
    {
        public string Host { get; set; } = string.Empty;
        public string VirtualHost { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
