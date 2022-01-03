using Bookshelf.Models;

namespace Bookshelf.Repositories
{
    public interface IShelfRepository : IRepository<Shelf>
    {
        IEnumerable<Book> GetBooks(int shelfId);
        IEnumerable<Shelf> GetAllWithBindings();
        IEnumerable<ShelfInfoSimple> GetShelvesNamesAndAmountOfBooks(int amount);

        /// <summary>
        /// Removes only links between entities
        /// </summary>
        void RemoveBooksFromShelf(int shelfId);
    }
}
