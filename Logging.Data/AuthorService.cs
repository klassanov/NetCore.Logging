using System.Collections.Generic;
using Logging.Interfaces.Data;
using Logging.Model;
using Microsoft.Extensions.Logging;

namespace Logging.Data
{
    public class AuthorService : IAuthorService
    {
        //Logger factory example
        private readonly ILogger logger;

        public AuthorService(ILoggerFactory loggerFactory)
        {
            //Specify the category
            //You can use the same category string in another classes, but logically group logs by the same category
            this.logger = loggerFactory.CreateLogger("Database");

        }

        public IEnumerable<Author> GetAllAuthors()
        {
            this.logger.LogWarning("I will return null");
            return null;
        }
    }
}
