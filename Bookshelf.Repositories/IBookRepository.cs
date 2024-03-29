﻿using Bookshelf.Models;

namespace Bookshelf.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        IEnumerable<Author> GetAuthors(int id);
        IEnumerable<BookInfoSimple> GetBooksSimpleInfo(int amount);
        IEnumerable<StausesInfo> GetStatusesInfo();
        void UpdateStatus(Book entity, BookStatus status);
    }
}
