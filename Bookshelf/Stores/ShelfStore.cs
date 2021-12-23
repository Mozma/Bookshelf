using Bookshelf.Models;
using System;

namespace Bookshelf.Stores
{
    public class ShelfStore
    {
        public event Action<Shelf> ShelfDeleted;
        public event Action<Shelf> ShelfChanged;
        public event Action<Shelf> ShelfCreated;

        public void CreateShelf(Shelf shelf)
        {
            ShelfCreated?.Invoke(shelf);
        }

        public void ChangeShelf(Shelf shelf)
        {
            ShelfChanged?.Invoke(shelf);
        }

        public void DeleteShelf(Shelf shelf)
        {
            ShelfDeleted?.Invoke(shelf);
        }
    }
}
