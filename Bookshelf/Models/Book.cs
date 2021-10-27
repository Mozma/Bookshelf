using System.Collections.Generic;

namespace Bookshelf.Models
{
    public class Book 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? PagesNumber { get; set; }
        public int? Year { get; set; }
        public int? BookImageId { get; set; }
        public Status Status { get; set; }

        public Image BookImage { get; set; }
        public List<BookBind> BookBinds { get; set; }
        
    }
}
