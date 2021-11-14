using System.Collections.Generic;
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

        public IEnumerable<Book> GetBooks()
        {
            this.logger.LogInformation("In the GetBooks method");

            var books = new List<Book>()
            {
                new Book{Title="Vinetu", Price=110m},
                new Book{Title="The great indian", Price=99m},
            };

            this.logger.LogInformation("Retrieved books: {@books}", books);

            return books;
        }
    }
}
