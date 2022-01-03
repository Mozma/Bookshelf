using Bookshelf.Models;

namespace Bookshelf.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        IEnumerable<Author> GetAuthors(int id);
    }
}
