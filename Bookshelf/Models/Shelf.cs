using System.Collections.Generic;

namespace Bookshelf.Models
{
    public class Shelf
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }

        public List<BookBind> BookBinds { get; set; }
    }
}