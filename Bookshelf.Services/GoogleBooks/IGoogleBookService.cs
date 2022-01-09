using Google.Apis.Books.v1;
using Google.Apis.Books.v1.Data;

namespace Bookshelf.Services
{
    public interface IGoogleBookService
    {
        public Volumes FindBook(string isbn);

    }
}