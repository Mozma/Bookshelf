namespace Bookshelf.Models
{
    public class BookBind : DomainObject
    {       
        public Author Author { get; set; }
        public Book Book { get; set; }
        public Shelf Shelf { get; set; }
        
    }
}
