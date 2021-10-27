namespace Bookshelf.Models
{
    public class BookBind
    {
        public int Id { get; set; }
        public Author Author { get; set; }
        public Book Book { get; set; }
        public Shelf Shelf { get; set; }
        
    }
}
