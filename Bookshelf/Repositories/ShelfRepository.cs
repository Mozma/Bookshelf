using Bookshelf.Models;
using Bookshelf.Models.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Bookshelf
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
                .ToList();
        }

        public IEnumerable<Book> GetBooks(int shelfId)
        {
            return Context.ShelfBinds
                .Include(o => o.Shelf)
                .Include(o => o.Book)
                .Where(o => o.Shelf.Id == shelfId)
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
    }
}
