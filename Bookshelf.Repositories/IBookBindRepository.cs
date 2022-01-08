using Bookshelf.Models;

namespace Bookshelf.Repositories
{
    public interface IBookBindRepository
    {
        void CreateIfNotExist(Book book, Author author);
        bool CheckIfLinkExist(string title, string authorName);
    }
}