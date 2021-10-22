

using System.Collections.Generic;

namespace Bookshelf.Models
{
    public class Author : DomainObject
    {
        public string FullName { get; set; }
        public int ImageId {  get; set; }

        public List<BookBind> BookBinds { get; set; }
    }
}
