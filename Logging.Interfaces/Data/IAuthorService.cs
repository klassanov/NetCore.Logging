using System.Collections.Generic;
using Logging.Model;

namespace Logging.Interfaces.Data
{
    public interface IAuthorService
    {
        IEnumerable<Author> GetAllAuthors();
    }
}
