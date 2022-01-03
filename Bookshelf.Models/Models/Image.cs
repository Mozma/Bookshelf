namespace Bookshelf.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Base64Data { get; set; }


        public List<Book> Books { get; set; }
        public List<Author> Authors { get; set; }
    }
}
