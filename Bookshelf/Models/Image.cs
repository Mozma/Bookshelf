using System.Collections.Generic;

namespace Bookshelf.Models
{
    public class Image
    {
        public int Id { get; set; }
        public byte[] Binary { get; set; }


        public List<Book> Books { get; set; }
        public List<Author> Authors { get; set; }
    }
}
