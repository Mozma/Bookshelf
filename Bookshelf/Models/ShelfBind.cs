namespace Bookshelf.Models
{
    public class ShelfBind
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public Shelf Shelf { get; set; }

    }
}
