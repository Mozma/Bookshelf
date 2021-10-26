using System.Collections.Generic;

namespace Bookshelf.Models
{
    public class Shelf : DomainObject
    {
        public string Name { get; set; }
        public Status Status { get; set; }

        public List<BookBind> BookBinds { get; set; }
    }
}