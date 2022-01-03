using Bookshelf.Models;
using Bookshelf.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace Bookshelf.Repositories
{
    public class ShelfRepository : Repository<Shelf>, IShelfRepository
    {
        public ShelfRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public IEnumerable<Shelf> GetAllWithBindings()
        {
            return Context.Set<Shelf>()
                .Include(o => o.ShelfBinds)
                .OrderBy(o => o.Name)
                .ToList();
        }

        public IEnumerable<Book> GetBooks(int shelfId)
        {
            return Context.ShelfBinds
                .Include(o => o.Shelf)
                .Include(o => o.Book)
                .Where(o => o.Shelf.Id == shelfId)
                .OrderBy(o => o.Book.Title)
                .Select(o => o.Book)
                .ToList();
        }

        public void RemoveBooksFromShelf(int shelfId)
        {
            var shelfBinds = Context.ShelfBinds
                .Where(o => o.Shelf.Id == shelfId)
                .ToList();

            Context.ShelfBinds.RemoveRange(shelfBinds);
            Context.SaveChanges();
        }

        public IEnumerable<ShelfInfoSimple> GetShelvesNamesAndAmountOfBooks(int amount)
        {
            return Context.Shelves
                .Include(o => o.ShelfBinds)
                .Select(o => new ShelfInfoSimple
                {
                    Id = o.Id,
                    Name = o.Name,
                    Amount = (o.ShelfBinds.Where(s => s.ShelfId == o.Id)).Count()
                })
                .OrderByDescending(x => x.Amount)
                .Take(amount)
                .ToList();
        }

    }
}
