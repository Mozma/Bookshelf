using Bookshelf.Models;

namespace Bookshelf.Tests
{
    public class ShelfBuilder
    {
        private int _id;
        private string _name;

        public ShelfBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public ShelfBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public Shelf Build()
        {
            return new Shelf(_id, _name ?? "");
        }

        public static implicit operator Shelf(ShelfBuilder builder)
        {
            return builder.Build();
        }
    }
}
