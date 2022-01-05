namespace Bookshelf.Models
{
    public class ShelfBind
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int ShelfId { get; set; }

        public Book Book { get; set; }
        public Shelf Shelf { get; set; }

        public ShelfBind() { }
        public ShelfBind(int shelfId, int bookId)
        {
            ShelfId = shelfId;
            BookId = bookId;
        }
    }
}
