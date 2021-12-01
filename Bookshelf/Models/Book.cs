using System.Collections.Generic;

namespace Bookshelf.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public int? PagesNumber { get; set; }
        public int? PagesRead { get; set; }
        public int? Year { get; set; }
        public int? ImageId { get; set; }
        public string ISBN { get; set; }
        public int? PublisherId { get; set; }
        public int? StatusId { get; set; }
        public string Description { get; set; }


        public Status Status { get; set; }
        public Image Image { get; set; }
        public Publisher Publisher { get; set; }

        public List<BookBind> BookBinds { get; set; }
        public List<ShelfBind> ShelfBinds { get; set; }

    }
}
