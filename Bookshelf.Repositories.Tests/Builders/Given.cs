
namespace Bookshelf.Tests
{
    public static class Given
    {
        public static ShelfBuilder Shelf => new ShelfBuilder();
        public static ShelfBindBuilder ShelfBind => new ShelfBindBuilder();
        public static BookBuilder Book => new BookBuilder();
    }
}
