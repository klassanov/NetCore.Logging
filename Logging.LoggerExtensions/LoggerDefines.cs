using System;
using Logging.Constants;
using Microsoft.Extensions.Logging;

namespace Logging.LoggerExtensions
{
    // Done for better performance. It has improvements in:
    // - memory allocation
    // - template parsing (done only once instead of each time)
    // - boxing (?)

    public static class LoggerDefines
    {
        private static readonly Action<ILogger, Exception> repoGetBooks;

        private static readonly Action<ILogger, string, Exception> repoGetMoreBooks;

        private static readonly Func<ILogger, string, IDisposable> getBooksScoped;

        static LoggerDefines()
        {
            repoGetBooks = LoggerMessage.Define(LogLevel.Information, 0, "RepoGetBooks call");
            repoGetMoreBooks = LoggerMessage.Define<string>(LogLevel.Debug, DataEvents.GetMany, "Debug info about stored proc {ProcName}");
            getBooksScoped = LoggerMessage.DefineScope<string>("Scope for retrieving the books for user {UserId}");
        }

        public static void RepoGetBooks(this ILogger logger)
        {
            repoGetBooks(logger, null);
        }

        public static void RepoGetMoreBooks(this ILogger logger, string procName)
        {
            repoGetMoreBooks(logger, procName, null);
        }

        public static IDisposable GetBooksScoped(this ILogger logger, string userId)
        {
            return getBooksScoped(logger, userId);
        }

    }
}
