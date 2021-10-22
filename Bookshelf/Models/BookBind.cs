namespace Bookshelf.Models
{
    public class BookBind : DomainObject
    {
        public int AuthorId { get; set; }
        public int BookId { get; set; }
        public int ShelfId { get; set; }

        
        public Author Author { get; set; }
        public Book Book { get; set; }
        public Shelf Shelf { get; set; }
        
    }
}
