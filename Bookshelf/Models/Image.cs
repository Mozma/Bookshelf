

using System.Collections.Generic;

namespace Bookshelf.Models
{
    public class Image : DomainObject
    {
        public byte[] Data { get; set; }


        public List<Book> Books { get; set; }
        public List<Author> Authors {  get; set; }
    }
}
