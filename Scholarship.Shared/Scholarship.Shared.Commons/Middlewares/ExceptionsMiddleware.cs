using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Shared.Commons.Middlewares
{
    public class ExceptionsMiddleware: object
    {
        private readonly RequestDelegate nextDelegate = default!;
        private ILogger<ExceptionsMiddleware> Logger { get; set; } = default!;

        public ExceptionsMiddleware(RequestDelegate next, ILogger<ExceptionsMiddleware> logger) : base()
        {
            this.nextDelegate = next;
            this.Logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try { await this.nextDelegate(context); }
            catch (Exception error)
            {
                this.Logger.LogWarning($"An exception occurred during the request: {error.GetType().Name}");
                this.Logger.LogWarning($"Exception message: {error.Message}");
                await HandleExceptionAsync(context, error);
            }
        }
        protected virtual async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var result = new ExceptionMessage(
                title: $"Exception type: {exception.GetType().Name}",
                errors: exception.Message
            );  
            await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
    }
    public record class ExceptionMessage(string title, string errors);
    public static class ExceptionsMiddlewareExtension : object
    {
        public static IApplicationBuilder UseExceptionsHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionsMiddleware>();
        }
    }
}
