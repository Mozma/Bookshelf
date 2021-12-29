using Bookshelf.Models;
using System.Collections.Generic;

namespace Bookshelf
{
    public interface IBookRepository : IRepository<Book>
    {
        IEnumerable<Author> GetAuthors(int id);
    }
}
