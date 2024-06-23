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
    public class ExceptionsMiddleware<TException> : object where TException : Exception
    {
        private readonly RequestDelegate nextDelegate = default!;
        private ILogger<ExceptionsMiddleware<TException>> Logger { get; set; } = default!;

        public ExceptionsMiddleware(RequestDelegate next, ILogger<ExceptionsMiddleware<TException>> logger) : base()
        {
            this.Logger = logger;
            this.nextDelegate = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try { await this.nextDelegate(context); }
            catch (TException error)
            {
                this.Logger.LogWarning(error, $"Возникло исключение при запросе: {error.Message}");
                await HandleExceptionAsync(context, error);
            }
        }
        protected virtual Task HandleExceptionAsync(HttpContext context, TException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var result = new
            {
                message = $"Возникло исключение при запросе: {exception.GetType().Name}",
                details = exception.Message
            };
            return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
    }
    public static class ExceptionsMiddlewareExtension : object
    {
        public static IApplicationBuilder UseExceptionsHandler<TException>(this IApplicationBuilder builder)
            where TException : Exception
        {
            return builder.UseMiddleware<ExceptionsMiddleware<TException>>();
        }
    }
}
