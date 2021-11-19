using System;
using Microsoft.AspNetCore.Http;

namespace Logging.API.Middleware
{
    public class ApiExceptionOptions
    {
        //Meant to add details to the response object if needed

        public Action<HttpContext, Exception, ApiError> AddResponseDetails { get; set; }
    }
}
