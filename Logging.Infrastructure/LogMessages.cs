using System;
using Microsoft.Extensions.Logging;

namespace Logging.Infrastructure
{
    public static class LogMessages
    {
        //Get a reference to the .Define result (define once, compliled) and then reuse it
        private static Action<ILogger, string, string, long, Exception> trackRoutePerformance;

        static LogMessages()
        {
            //Define once in the constructor
            trackRoutePerformance = LoggerMessage.Define<string, string, long>(LogLevel.Information, 0, "{RouteName} {Method} took {ElapsedMilliseconds}");
        }

        public static void LogRoutePerformance(this ILogger logger, string routeName, string method, long elapsedMilliseconds)
        {
            //Use it any time once it is defined
            trackRoutePerformance(logger, routeName, method, elapsedMilliseconds, null);
        }
    }
}
