using System.Collections.Generic;
using Logging.Constants;
using Logging.Interfaces.Data;
using Logging.Model;
using Microsoft.Extensions.Logging;

namespace Logging.Data
{
    public class BookService : IBookService
    {
        private readonly ILogger<BookService> logger;

        public BookService(ILogger<BookService> logger)
        {
            this.logger = logger;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            this.logger.LogTrace("This is a trace message");
            this.logger.LogInformation("In the GetBooks method");
            this.logger.LogDebug(DataEvents.GetMany, "Debugging info");
            this.logger.LogWarning("This is a warning not from db");
            this.logger.LogError("This is an error");
            this.logger.LogCritical("This is a critical error");

            List<Book> books = new List<Book>();

            //books.AddRange(new List<Book>
            //{
            //    new Book{Title="Vinetu", Price=110m},
            //    new Book{Title="The great indian", Price=99m},
            //});

            if (books.Count == 0)
            {
                //this.logger.LogWarning("No books retrieved from the db");
            }

            //this.logger.LogInformation("Retrieved {books} ", books);

            return books;
        }


        public IEnumerable<Book> GetMoreBooks()
        {


            return null;
        }

    }


}
