using System.Collections.Generic;
using Logging.Interfaces.Data;
using Logging.Model;

namespace Logging.Data
{
    public class BookService : IBookService
    {
        public IEnumerable<Book> GetBooks()
        {
            return new List<Book>()
            {
                new Book{Title="Vinetu", Price=110m},
                new Book{Title="The great indian", Price=99m},
            };
        }
    }
}
