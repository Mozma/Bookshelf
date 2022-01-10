using Bookshelf.Models;
using Bookshelf.Models.Data;

namespace Bookshelf.Repositories
{
    public class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public Publisher GetOrCreatePublisherWithName(string name)
        {
            var publisher = Context.Publishers.FirstOrDefault(a => string.Equals(a.Name.ToUpper(), name.Trim().ToUpper()));

            if (publisher == null)
            {
                publisher = Context.Publishers.Add(new Publisher { Name = name.Trim() }).Entity;
                Context.SaveChanges();
            }

            return publisher;
        }

        public IEnumerable<string> GetNames() {
            return Context.Publishers.Select(o => o.Name).ToList();
        }
    }
}