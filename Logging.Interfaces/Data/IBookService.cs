using System.Collections.Generic;
using Logging.Model;

namespace Logging.Interfaces.Data
{
    public interface IBookService
    {
        IEnumerable<Book> GetBooks();
    }
}
