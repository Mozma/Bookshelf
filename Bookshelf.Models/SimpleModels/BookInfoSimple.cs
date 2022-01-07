namespace Bookshelf.Models
{
    public class BookInfoSimple
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public BookStatus BookStatus { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
