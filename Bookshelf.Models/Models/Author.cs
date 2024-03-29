﻿namespace Bookshelf.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int? AuthorImageId { get; set; }
        public string Country { get; set; }


        public Image AuthorImage { get; set; }
        public List<BookBind> BookBinds { get; set; }
    }
}
