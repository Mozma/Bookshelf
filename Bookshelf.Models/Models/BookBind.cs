namespace Bookshelf.Models
{
    public class BookBind
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }
        public int BookId { get; set; }

        public Author Author { get; set; }
        public Book Book { get; set; }
    }
}
