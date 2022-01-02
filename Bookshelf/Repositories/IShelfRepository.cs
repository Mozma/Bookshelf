using Bookshelf.Models;
using System.Collections.Generic;

namespace Bookshelf
{
    public interface IShelfRepository : IRepository<Shelf>
    {
        IEnumerable<Book> GetBooks(int shelfId);
        IEnumerable<Shelf> GetAllWithBindings();
        IEnumerable<object> GetShelvesNamesAndAmountOfBooks(int amount);

        /// <summary>
        /// Removes only links between entities
        /// </summary>
        void RemoveBooksFromShelf(int shelfId);
    }
}
