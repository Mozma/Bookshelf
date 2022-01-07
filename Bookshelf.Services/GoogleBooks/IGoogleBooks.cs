using Bookshelf.Models;

namespace Bookshelf.Services
{
    public interface IGoogleBooks
    {
        public Task<BookInfo> FindBookByIsbnAsync(string isbn);
    }
}