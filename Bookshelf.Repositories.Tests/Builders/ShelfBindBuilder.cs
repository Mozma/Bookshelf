using Bookshelf.Models;

namespace Bookshelf.Tests
{
    public class ShelfBindBuilder
    {
        private int _id;
        private int _shelfId;
        private int _bookId;

        public ShelfBindBuilder WithId(int id)
        {
            _id = id;
            return this;
        }
        public ShelfBindBuilder WithShelfId(int id)
        {
            _shelfId = id;
            return this;
        }
        public ShelfBindBuilder WithBookId(int id)
        {
            _bookId = id;
            return this;
        }
    

        public ShelfBind Build()
        {
            return new ShelfBind(_shelfId, _bookId);
        }

        public static implicit operator ShelfBind(ShelfBindBuilder builder)
        {
            return builder.Build();
        }
    }
}
