namespace Bookshelf.Models
{
    public class BookInfo
    {
        public string Title { get; set; }
        public int? PagesNumber { get; set; }
        public int? PagesRead { get; set; }
        public int? Year { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public Image Cover { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
