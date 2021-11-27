using System;
using Microsoft.AspNetCore.Builder;

namespace Logging.Infrastructure.Middleware
{
    public static class ApiExceptionMiddlewareExtensions
    {
        //Extension methods to attach it to the middleware

        public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder builder)
        {
            var options = new ApiExceptionOptions();
            return builder.UseMiddleware<ApiExceptionMiddleware>(options);
        }

        public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder builder, Action<ApiExceptionOptions> configureOptions)
        {
            var options = new ApiExceptionOptions();
            configureOptions(options);
            return builder.UseMiddleware<ApiExceptionMiddleware>(options);
        }
    }
}
