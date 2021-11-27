using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Logging.Infrastructure.Middleware
{
    public class ApiExceptionMiddleware
    {
        //The actual middleware where actual work happens

        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionMiddleware> _logger;
        private readonly ApiExceptionOptions _options;

        public ApiExceptionMiddleware(RequestDelegate next, ILogger<ApiExceptionMiddleware> logger, ApiExceptionOptions options)
        {
            this._next = next;
            this._logger = logger;
            this._options = options;
        }

        public async Task Invoke(HttpContext context /*other dependencies*/)
        {
            //just proxies the request to the next piece of middleware, but catches
            //if there is an exception and stops the request lifecycle
            try
            {
                await this._next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, this._options);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex, ApiExceptionOptions options)
        {
            //Will be returned to caller
            //Can be shown on the caller's UI -> especially the Id, which details will also be logged with the same Id
            var error = new ApiError()
            {
                Id = Guid.NewGuid().ToString(),
                Status = (short)HttpStatusCode.InternalServerError,
                Title = "API Error occurred"
            };

            options.AddResponseDetails?.Invoke(context, ex, error);

            var innerExceptionMessage = GetInnermostExceptionMessage(ex);

            //We log an error
            this._logger.LogError(ex, $"BADNESS {innerExceptionMessage} -- ErrorId {error.Id}");

            //Create the result and write it to the repsonse
            var result = JsonConvert.SerializeObject(error);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(result);
        }

        private string GetInnermostExceptionMessage(Exception ex)
        {
            if (ex.InnerException != null)
            {
                GetInnermostExceptionMessage(ex.InnerException);
            }

            return ex.Message;
        }
    }
}
