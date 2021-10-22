using System.Collections.Generic;

namespace Bookshelf.Models
{
    public class Status : DomainObject
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Book> Books { get; set; }
        public List<Shelf> Shelves { get; set; }
    }
}