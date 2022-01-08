using Bookshelf.Models;

namespace Bookshelf.Repositories
{
    public interface IAuthorRepository
    {
        Author GetOrCreateAuthorWithName(string author);
    }
}