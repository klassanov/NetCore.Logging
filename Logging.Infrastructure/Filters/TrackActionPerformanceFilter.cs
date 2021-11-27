using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Logging.Infrastructure.Filters
{
    public class TrackActionPerformanceFilter : IActionFilter
    {
        private readonly ILogger<TrackActionPerformanceFilter> logger;
        private Stopwatch timer;

        public TrackActionPerformanceFilter(ILogger<TrackActionPerformanceFilter> logger)
        {
            this.logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //this.timer = new Stopwatch();
            //this.timer.Start()
            //is the same as writing

            this.timer = Stopwatch.StartNew();
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {
            this.timer.Stop();

            //We log when exceptions do not occur
            if (context.Exception == null)
            {
                LogMessages.LogRoutePerformance(logger,
                    context.ActionDescriptor.DisplayName,
                    context.HttpContext.Request.Method,
                    this.timer.ElapsedMilliseconds);
            }
        }
    }
}
