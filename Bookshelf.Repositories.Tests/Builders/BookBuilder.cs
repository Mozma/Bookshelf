using Bookshelf.Models;

namespace Bookshelf.Tests
{
    public class BookBuilder
    {
        private int _id;
        private string _title;

        public BookBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public BookBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public Book Build()
        {
            return new Book(_id, _title ?? "");
        }

        public static implicit operator Book(BookBuilder builder)
        {
            return builder.Build();
        }
    }
}
