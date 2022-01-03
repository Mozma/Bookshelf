using Bookshelf.Models;
using Bookshelf.Models.Data;
using Microsoft.EntityFrameworkCore;
//using System.Linq;

namespace Bookshelf.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public IEnumerable<Author> GetAuthors(int id)
        {
            return Context.BookBinds
                 .Include(o => o.Book)
                 .Include(o => o.Author)
                 .Where(o => o.Book.Id == id)
                 .Select(o => o.Author)
                 .OrderBy(o => o.FullName)
                 .ToList();
        }
    }
}
