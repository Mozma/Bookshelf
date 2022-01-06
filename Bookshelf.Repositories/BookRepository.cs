﻿using Bookshelf.Models;
using Bookshelf.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace Bookshelf.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public IEnumerable<Author> GetAuthors(int id)
        {
            return Context.BookBinds
                 .Include(o => o.Book)
                 .Include(o => o.Author)
                 .Where(o => o.Book.Id == id)
                 .Select(o => o.Author)
                 .OrderBy(o => o.FullName)
                 .ToList();
        }
        public IEnumerable<BookInfoSimple> GetBooksSimpleInfo(int amount = 12)
        {
            return Context.Books
                .Include(o => o.BookBinds)
                .Select(o => new BookInfoSimple
                {
                    Id = o.Id,
                    Title = o.Title,
                    Author = o.BookBinds.First().Author.FullName,
                    BookStatus = o.Status == null ? BookStatus.WithoutStatus : (BookStatus)o.Status,
                    CreationTime = o.CreationTime
                })
                .OrderByDescending(x => x.CreationTime)
                .Take(amount)
                .ToList();
        }
    }
}
