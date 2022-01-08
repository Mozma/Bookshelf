using Bookshelf.Models;

namespace Bookshelf.Repositories
{
    public interface IPublisherRepository
    {
        Publisher GetOrCreatePublisherWithName(string publisher);
    }
}