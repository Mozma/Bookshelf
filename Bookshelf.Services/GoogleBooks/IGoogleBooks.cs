using Bookshelf.Models;

namespace Bookshelf.Services
{
    public interface IGoogleBooks
    {
        public BookInfo FindBookByIsbn(string isbn);
    }
}